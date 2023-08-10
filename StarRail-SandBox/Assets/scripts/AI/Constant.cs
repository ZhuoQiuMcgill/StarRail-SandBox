using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constant
{
    public class Constant
    {
        //Star
        public const int defaultMinInitialStarCitizen = 1;
        public const int defaultMaxInitialStarCitizen = 5;
        public const int defaultCitizenConsume = 10;
        public const int defaultStarFoodProductionRate = 2 * defaultCitizenConsume;

        //War
        public const int CitizenToWarrior = 1;
        public const int CitizenToWarriorRate = 3;
        public const int WarriorMoveSpeed = 25;

        //Building
        public const int defaultCitizenEfficiency = 1;
        public const int outpostBuildingTime = 3;
        public const int spaceStationBuildingTime = 5;
        public const int barracksBuildingTime = 5;

        //±øÖÖÊýÖµ
        public const int defaultMinionATK = 10;
        public const int defaultMinionDEF = 10;


        public static int DamageCalculation(int ATK, int DEF)
        {
            return ATK - DEF;
        }
    }
}

