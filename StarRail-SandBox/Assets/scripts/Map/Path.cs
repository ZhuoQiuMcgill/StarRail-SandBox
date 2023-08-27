using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MapElement
{
    public class Path
    {
        public Star star1;
        public Star star2;
        public double speedRate { get; set; } = 1.0f;
        public bool unionPath { get; set; } = false;
        public double distance { get; }

        public Path(Star star1, Star star2)
        {
            this.star1 = star1;
            this.star2 = star2;
            this.distance = Vector2.Distance(star1.pos, star2.pos);
        }
    }
}
