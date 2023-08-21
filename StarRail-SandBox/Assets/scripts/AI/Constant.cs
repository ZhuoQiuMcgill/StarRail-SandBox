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

        //兵种数值
        public const int defaultMinionATK = 10;
        public const int defaultMinionDEF = 10;

        //派系颜色
        public const int destructionColor = 0x606060;
        public const int huntColor = 0x00ff80;
        public const int abundanceColor = 0xffff00;
        public const int preservationColor = 0xcc6600;
        public const int eruditionColor = 0x004c99;
        public const int nihilityColor = 0x4c0099;
        public const int harmonyColor = 0xff66ff;
        public const int trailBlazeColor = 0xff0000;
        


        public static int DamageCalculation(int ATK, int DEF)
        {
            return ATK - DEF;
        }
    }
}

