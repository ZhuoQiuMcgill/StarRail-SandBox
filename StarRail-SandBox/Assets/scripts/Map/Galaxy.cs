using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

using Path;
using Star;

namespace Galaxy
{
    public class Galaxy
    {
        public int numStars;
        public int width;
        public int height;

        public List<Star.Star> stars { get; } = new List<Star.Star>();
        public List<Path.Path> paths { get; } = new List<Path.Path>();

        private Dictionary<Star.Star, Star.Star> parent = new Dictionary<Star.Star, Star.Star>();
        private Dictionary<Star.Star, int> rank = new Dictionary<Star.Star, int>();

        private double livableRate = 0.2;           // 生成宜居星系的概率
        private double blackholeRate = 0.05;        // 生成黑洞星系的概率
        private List<List<List<Star.Star>>> grid;   // 地图分区
        private int gridSize = 100;                 // 分区大小
        private float[] pathRate = new float[] { 1.0f, 1.0f, 0.2f };
        private int maxDegree;                      // 每个星系的最大路径
        private int maxConnection = 10;
        private double minDistance = 30.0;


        public Galaxy(int numStars, int width, int height)
        {
            this.numStars = numStars;
            this.width = width;
            this.height = height;
            this.maxDegree = pathRate.Length;
            GenerateStarsAndPaths();
        }

        /** 
         * 初始化地图分区
         */
        private void InitGrid()
        {
            // 计算前两维的大小
            int xPos = this.width / this.gridSize;
            int yPos = this.height / this.gridSize;

            // 初始化前两维
            grid = new List<List<List<Star.Star>>>();
            for (int i = 0; i < xPos; i++)
            {
                List<List<Star.Star>> twoDimensionalList = new List<List<Star.Star>>();
                for (int j = 0; j < yPos; j++)
                {
                    // 对第三维进行空初始化，只有在需要时才添加元素
                    twoDimensionalList.Add(new List<Star.Star>());
                }
                grid.Add(twoDimensionalList);
            }
        }

        /** 
         * 这个method会寻找距离star最近的n个星系并返回
         */
        public Star.Star[] CloestStars(Star.Star star, int n)
        {
            Vector2 starPos = star.pos;
            int xPos = (int)(starPos.x / this.gridSize);
            int yPos = (int)(starPos.y / this.gridSize);

            int widthLimit = this.width / this.gridSize;
            int heightLimit = this.height / this.gridSize;

            List<Star.Star> potentialClosestStars = new List<Star.Star>();

            int layer = 0;

            while (layer < Math.Max(widthLimit, heightLimit))
            {
                for (int x = xPos - layer; x <= xPos + layer; x++)
                {
                    for (int y = yPos - layer; y <= yPos + layer; y++)
                    {
                        if (x < 0 || x >= widthLimit || y < 0 || y >= heightLimit) continue;
                        if (x > xPos - layer && x < xPos + layer && y > yPos - layer && y < yPos + layer) continue;

                        foreach (Star.Star adjStar in this.grid[x][y])
                        {
                            if (adjStar == star) continue; // skip the input star
                            if (!potentialClosestStars.Contains(adjStar))
                            {
                                potentialClosestStars.Add(adjStar);
                            }
                        }
                    }
                }
                layer++;

                if (potentialClosestStars.Count >= n) break;
            }

            return potentialClosestStars
                .OrderBy(s => Vector2.Distance(star.pos, s.pos))
                .Take(n)
                .ToArray();
        }



        /**
         * 生成星系以及路径，将数据存储至stars和paths
         */
        public void GenerateStarsAndPaths()
        {
            InitGrid();

            // 生成星系
            for (int i = 0; i < numStars;)
            {
                float radius = height / 2.0f;
                float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                float distanceFromCenter = Mathf.Sqrt(UnityEngine.Random.Range(0, radius * radius)); // 使用平方根函数保证随机分布是均匀的
                Vector2 pos = new Vector2(radius + distanceFromCenter * Mathf.Cos(angle), radius + distanceFromCenter * Mathf.Sin(angle));

                int xPos = (int)(pos.x / this.gridSize);
                int yPos = (int)(pos.y / this.gridSize);

                bool isTooClose = false;

                // Calculate the range of grid cells to check, considering the minDistance.
                int offset = (int)Math.Ceiling(this.minDistance / this.gridSize);
                int xStart = Math.Max(xPos - offset, 0);
                int xEnd = Math.Min(xPos + offset, this.grid.Count - 1);
                int yStart = Math.Max(yPos - offset, 0);
                int yEnd = Math.Min(yPos + offset, this.grid[0].Count - 1);

                for (int x = xStart; x <= xEnd && !isTooClose; x++)
                {
                    for (int y = yStart; y <= yEnd && !isTooClose; y++)
                    {
                        foreach (Star.Star existingStar in this.grid[x][y])
                        {
                            if (Vector2.Distance(pos, existingStar.pos) < this.minDistance)
                            {
                                isTooClose = true;
                                break;
                            }
                        }
                    }
                }

                if (!isTooClose)
                {
                    Star.Star star = new Star.Star(i, pos, this.livableRate, this.blackholeRate);
                    this.stars.Add(star);
                    this.grid[xPos][yPos].Add(star);
                    i++; // Only increment if we successfully placed a star
                }
            }



            // 连接星系，生成路径
            foreach (Star.Star star in this.stars)
            {
                Star.Star[] cloestStars = CloestStars(star, this.maxDegree);
                for (int i = 0; i < this.maxDegree; i++)
                {
                    if (i > 0) 
                    {
                        if (star.adj.Count > this.maxDegree) { break; }
                        if (cloestStars[i].adj.Count > this.maxDegree) { continue; }
                    }
                    float randomNumber = UnityEngine.Random.Range(0f, 1f);
                    if (randomNumber < this.pathRate[i])
                    {
                        this.paths.Add(new Path.Path(star, cloestStars[i]));
                        star.adj.Add(cloestStars[i]);
                    }
                }
            }


            
            // 使用并查集确保所有星系的连通性
            // 1. 初始化
            foreach (Star.Star star in this.stars)
            {
                parent[star] = star;
                rank[star] = 0;
            }

            // 2. 使用Union合并
            foreach (Path.Path path in this.paths)
            {
                Union(path.star1, path.star2);
            }

            // 3. 找出所有的代表星系
            HashSet<Star.Star> representatives = new HashSet<Star.Star>();
            foreach (Star.Star star in this.stars)
            {
                representatives.Add(Find(star));
            }

            // 4. 为每对独立的代表星系添加最短路径
            while (representatives.Count > 1)
            {
                Star.Star[] reps = representatives.ToArray();

                // 创建一个列表来存储所有距离及其对应的星系对
                List<Tuple<double, Star.Star, Star.Star>> distances = new List<Tuple<double, Star.Star, Star.Star>>();

                // 对于每一对Union，计算它们之间的所有星系对的距离
                foreach (Star.Star repA in reps)
                {
                    foreach (Star.Star repB in reps)
                    {
                        if (Find(repA) != Find(repB))  // 确保它们属于不同的Union
                        {
                            foreach (Star.Star aMember in this.stars.Where(s => Find(s) == repA))
                            {
                                foreach (Star.Star bMember in this.stars.Where(s => Find(s) == repB))
                                {
                                    double distance = Vector2.Distance(aMember.pos, bMember.pos);
                                    distances.Add(new Tuple<double, Star.Star, Star.Star>(distance, aMember, bMember));
                                }
                            }
                        }
                    }
                }

                // 对距离进行排序并选择maxConnection个最小的距离
                var sortedDistances = distances.OrderBy(tuple => tuple.Item1).Take(this.maxConnection).ToList();

                // 如果没有找到可以连接的星系，则退出循环
                if (sortedDistances.Count == 0)
                {
                    break;
                }

                foreach (var tuple in sortedDistances)
                {
                    Path.Path newPath = new Path.Path(tuple.Item2, tuple.Item3);
                    newPath.unionPath = true;
                    this.paths.Add(newPath);
                    tuple.Item2.adj.Add(tuple.Item3);
                    tuple.Item3.adj.Add(tuple.Item2);

                    Union(tuple.Item2, tuple.Item3);

                    representatives.Remove(Find(tuple.Item2));
                    representatives.Remove(Find(tuple.Item3));
                    representatives.Add(Find(tuple.Item2));
                }
            }

        }




        private Star.Star Find(Star.Star star)
        {
            if (parent[star] != star)
            {
                parent[star] = Find(parent[star]);  // Path compression
            }
            return parent[star];
        }

        private void Union(Star.Star star1, Star.Star star2)
        {
            Star.Star root1 = Find(star1);
            Star.Star root2 = Find(star2);

            // Union by rank
            if (root1 != root2)
            {
                if (rank[root1] > rank[root2])
                {
                    parent[root2] = root1;
                }
                else if (rank[root1] < rank[root2])
                {
                    parent[root1] = root2;
                }
                else
                {
                    parent[root1] = root2;
                    rank[root2]++;
                }
            }
        }




    }

}


