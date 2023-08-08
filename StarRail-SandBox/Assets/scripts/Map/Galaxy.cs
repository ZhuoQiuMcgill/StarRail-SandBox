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

        private double livableRate = 0.2;           // �����˾���ϵ�ĸ���
        private double blackholeRate = 0.05;        // ���ɺڶ���ϵ�ĸ���
        private List<List<List<Star.Star>>> grid;   // ��ͼ����
        private int gridSize = 100;                 // ������С
        private float[] pathRate = new float[] { 1.0f, 1.0f, 0.2f };
        private int maxDegree;                      // ÿ����ϵ�����·��
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
         * ��ʼ����ͼ����
         */
        private void InitGrid()
        {
            // ����ǰ��ά�Ĵ�С
            int xPos = this.width / this.gridSize;
            int yPos = this.height / this.gridSize;

            // ��ʼ��ǰ��ά
            grid = new List<List<List<Star.Star>>>();
            for (int i = 0; i < xPos; i++)
            {
                List<List<Star.Star>> twoDimensionalList = new List<List<Star.Star>>();
                for (int j = 0; j < yPos; j++)
                {
                    // �Ե���ά���пճ�ʼ����ֻ������Ҫʱ�����Ԫ��
                    twoDimensionalList.Add(new List<Star.Star>());
                }
                grid.Add(twoDimensionalList);
            }
        }

        /** 
         * ���method��Ѱ�Ҿ���star�����n����ϵ������
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
         * ������ϵ�Լ�·���������ݴ洢��stars��paths
         */
        public void GenerateStarsAndPaths()
        {
            InitGrid();

            // ������ϵ
            for (int i = 0; i < numStars;)
            {
                float radius = height / 2.0f;
                float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                float distanceFromCenter = Mathf.Sqrt(UnityEngine.Random.Range(0, radius * radius)); // ʹ��ƽ����������֤����ֲ��Ǿ��ȵ�
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



            // ������ϵ������·��
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


            
            // ʹ�ò��鼯ȷ��������ϵ����ͨ��
            // 1. ��ʼ��
            foreach (Star.Star star in this.stars)
            {
                parent[star] = star;
                rank[star] = 0;
            }

            // 2. ʹ��Union�ϲ�
            foreach (Path.Path path in this.paths)
            {
                Union(path.star1, path.star2);
            }

            // 3. �ҳ����еĴ�����ϵ
            HashSet<Star.Star> representatives = new HashSet<Star.Star>();
            foreach (Star.Star star in this.stars)
            {
                representatives.Add(Find(star));
            }

            // 4. Ϊÿ�Զ����Ĵ�����ϵ������·��
            while (representatives.Count > 1)
            {
                Star.Star[] reps = representatives.ToArray();

                // ����һ���б����洢���о��뼰���Ӧ����ϵ��
                List<Tuple<double, Star.Star, Star.Star>> distances = new List<Tuple<double, Star.Star, Star.Star>>();

                // ����ÿһ��Union����������֮���������ϵ�Եľ���
                foreach (Star.Star repA in reps)
                {
                    foreach (Star.Star repB in reps)
                    {
                        if (Find(repA) != Find(repB))  // ȷ���������ڲ�ͬ��Union
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

                // �Ծ����������ѡ��maxConnection����С�ľ���
                var sortedDistances = distances.OrderBy(tuple => tuple.Item1).Take(this.maxConnection).ToList();

                // ���û���ҵ��������ӵ���ϵ�����˳�ѭ��
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


