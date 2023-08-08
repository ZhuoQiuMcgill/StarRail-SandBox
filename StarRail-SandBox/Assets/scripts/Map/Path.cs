using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Star;

namespace Path
{
    public class Path
    {
        public Star.Star star1;
        public Star.Star star2;
        public double speedRate { get; set; } = 1.0f;
        public bool unionPath { get; set; } = false;

        public Path(Star.Star star1, Star.Star star2)
        {
            this.star1 = star1;
            this.star2 = star2;
        }
    }
}
