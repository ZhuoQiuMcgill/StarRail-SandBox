using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Path;
using Star;

namespace Galaxy
{
    public class Galaxy
    {
        public int numStars;
        public int width;
        public int height;

        public List<Star.Star> stars = new List<Star.Star>();
        public List<Path.Path> paths = new List<Path.Path>();

        private Dictionary<Star.Star, Star.Star> parent = new Dictionary<Star.Star, Star.Star>();
        private Dictionary<Star.Star, int> rank = new Dictionary<Star.Star, int>();
        private double livableRate = 0.2;       // 生成宜居星系的概率
        private double blackholeRate = 0.05;    // 生成黑洞星系的概率

        public Galaxy(int numStars, int width, int height)
        {
            this.numStars = numStars;
            this.width = width;
            this.height = height;
            GenerateStarsAndPaths();
        }

        public void GenerateStarsAndPaths()
        {
            // Generate stars
            for (int i = 0; i < numStars; i++)
            {
                Vector2 pos = new Vector2(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height));
                Star.Star star = new Star.Star(i, pos, this.livableRate, this.blackholeRate);
                stars.Add(star);
            }

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


