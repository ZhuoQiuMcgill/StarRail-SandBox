using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class Warrior : MonoBehaviour
    {
        public int ATK { get; }
        public int DEF { get; }
        public int speed { get; }
    }

    public class WarriorGroup
    {
        public List<Warrior> warriors { get; }
        public int totalATK { get; }
        public int totalDEF { get; }
        public int shield { get; set; }

        public WarriorGroup (List<Warrior> warriors)
        {
            this.warriors = warriors;
            foreach (Warrior w in warriors)
            {
                this.totalATK += w.ATK;
                this.totalDEF += w.DEF;
            }
            this.shield = 0;
        }

        
    }
}

