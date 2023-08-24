using System.Collections.Generic;
using UnityEngine;
using MapElement;

namespace VoronoiGenerator
{
    public class Voronoi
    {
        public int width;
        public int height;
        public Color[] voronoiColors; // 假设您有一个Color数组来表示每个星的颜色

        public Voronoi(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Color[,] GenerateVoronoi(List<Star> stars)
        {
            Color[,] voronoiMap = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Star nearestStar = FindNearestStar(new Vector2(x, y), stars);
                    voronoiMap[x, y] = voronoiColors[nearestStar.id]; // 假设Star的id作为颜色数组的索引
                }
            }

            return voronoiMap;
        }

        private Star FindNearestStar(Vector2 point, List<Star> stars)
        {
            float minDistance = float.MaxValue;
            Star nearestStar = null;

            foreach (Star star in stars)
            {
                float distance = Vector2.Distance(point, star.pos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestStar = star;
                }
            }

            return nearestStar;
        }
    }
}
