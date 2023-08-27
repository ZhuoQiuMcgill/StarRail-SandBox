using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MapElement;
using Constant;

namespace Warrior
{
    public class Warrior : MonoBehaviour
    {
        public int ATK { get; }
        public int DEF { get; }
        public int speed { get; }

        public Warrior (int ATK, int DEF, int speed)
        {
            this.ATK = ATK;
            this.DEF = DEF;
            this.speed = speed;
        }
    }

    public class WarriorGroup
    {
        public List<Warrior> warriors { get; set; }
        public int totalATK { get; set; }
        public int totalDEF { get; set; }
        public int shield { get; set; }

        public MapElement.Star star { get; set; }

        public Constant.civNames belongsTo { get; set; }

        public bool IsMoving { get; set; }

        public WarriorGroup (List<Warrior> warriors, Constant.civNames civ)
        {
            this.belongsTo = civ;
            this.warriors = warriors;
            foreach (Warrior w in warriors)
            {
                this.totalATK += w.ATK;
                this.totalDEF += w.DEF;
            }
            this.shield = 0;
        }

        public void addWarrior (Warrior warrior)
        {
            this.warriors.Add(warrior);
            this.totalATK += warrior.ATK;
            this.totalDEF += warrior.DEF;
        }



        
    }
}

