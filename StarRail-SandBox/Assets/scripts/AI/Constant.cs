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
        public Vector3 destructionColor = new Vector3(0.372f, 0.372f, 0.380f);
        public Vector3 huntColor = new Vector3(0.252f, 0.850f, 0.552f);
        public Vector3 abundanceColor = new Vector3(0.930f, 0.895f, 0.156f);
        public Vector3 preservationColor = new Vector3(0.705f, 0.410f, 0.121f);
        public Vector3 eruditionColor = new Vector3(0.122f, 0.360f, 0.660f);
        public Vector3 nihilityColor = new Vector3(0.423f, 0.130f, 0.495f);
        public Vector3 harmonyColor = new Vector3(0.988f, 0.134f, 0.990f);
        public Vector3 trailBlazeColor = new Vector3(0.990f, 0.000f, 0.019f);



        public static int DamageCalculation(int ATK, int DEF)
        {
            return ATK - DEF;
        }
    }
}

