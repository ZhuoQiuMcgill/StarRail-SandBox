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

        public Galaxy(int numStars, int width, int height)
        {
            this.numStars = numStars;
            this.width = width;
            this.height = height;
        }

    }
}


