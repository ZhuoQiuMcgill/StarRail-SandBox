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
        private float[] pathRate = new float[] { 1.0f, 0.8f, 0.8f, 0.4f };
        private int maxDegree;    // 每个星系的最大路径


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

            List<Star.Star> closestStars = new List<Star.Star>();

            int layer = 0;

            while (closestStars.Count < n && layer < Math.Max(widthLimit, heightLimit))
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
                            closestStars.Add(adjStar);
                        }

                        if (closestStars.Count >= n) break;
                    }
                    if (closestStars.Count >= n) break;
                }
                layer++;
            }

            return closestStars.OrderBy(s => Vector2.Distance(star.pos, s.pos)).Take(n).ToArray();
        }


        /**
         * 生成星系以及路径，将数据存储至stars和paths
         */
        public void GenerateStarsAndPaths()
        {
            InitGrid();

            // 生成星系
            for (int i = 0; i < numStars; i++)
            {
                Vector2 pos = new Vector2(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height));

                // 计算出星系所属的格子坐标
                int xPos = (int)(pos.x / this.gridSize);
                int yPos = (int)(pos.y / this.gridSize);

                Star.Star star = new Star.Star(i, pos, this.livableRate, this.blackholeRate);
                this.stars.Add(star);
                this.grid[xPos][yPos].Add(star);
            }


            // TODO: 完善路径生成算法








            // Initialize the disjoint set
            foreach (Star.Star star in stars)
            {
                parent[star] = star;
                rank[star] = 0;
            }

            // Generate all possible paths and sort them by length
            List<Path.Path> possiblePaths = new List<Path.Path>();
            for (int i = 0; i < numStars - 1; i++)
            {
                for (int j = i + 1; j < numStars; j++)
                {
                    Path.Path path = new Path.Path(stars[i], stars[j]);
                    possiblePaths.Add(path);
                }
            }
            possiblePaths.Sort((p1, p2) => Vector2.Distance(p1.star1.pos, p1.star2.pos).CompareTo(Vector2.Distance(p2.star1.pos, p2.star2.pos)));

            // Apply Kruskal's Algorithm to find the minimum spanning tree
            foreach (Path.Path path in possiblePaths)
            {
                if (Find(path.star1) != Find(path.star2)) // Check if two stars are in the same set
                {
                    paths.Add(path);
                    Union(path.star1, path.star2); // Merge two sets
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


