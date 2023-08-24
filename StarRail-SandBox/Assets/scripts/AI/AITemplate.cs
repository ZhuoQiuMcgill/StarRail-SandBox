using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MapElement;
using Constant;
using Emanator;
using Warrior;

public class AITemplate : MonoBehaviour
{
    public List<MapElement.Star> ownedStars { get; } = new List<MapElement.Star>();
    public List<Emanator.Emanator> emanators = new List<Emanator.Emanator>();
    public List<Warrior.WarriorGroup> warriors = new List<Warrior.WarriorGroup>();
    public List<MapElement.Star> viewedStars { get; }
    

    private float aggressivity = 0.4f;

    //资源
    private int totalfood = 0;
    private int totalMetal = 0;
    private int totalEnergy = 0;
    private int totalPopulation = 0;
    private int totalWarriors = 0;
    


    //资源权重
    private int populationWeight = 0;
    private int metalWeight = 0;
    private int energyWeight = 0;
    private int foodWeight = 0;

    //视野范围
    private float vision = 150.0f;

    //占领星球
    public void buildStar(MapElement.Star star)
    {
        this.ownedStars.Add(star);
        this.totalfood += star.resources[MapResources.Resources.Food];
        this.totalMetal += star.resources[MapResources.Resources.Metal];
        this.totalEnergy += star.resources[MapResources.Resources.Energy];
        this.totalPopulation += star.resources[MapResources.Resources.Population];
    }

    //生产士兵
    public void createWarriors (MapElement.Star star, int num)
    {
        if (num > this.totalPopulation)
        {
            return;
        }
        this.totalPopulation -= num;
        foreach (Warrior.WarriorGroup g in this.warriors)
        {
            if (g.star.id == star.id)
            {
                for (int i = 0; i < num; i++)
                {
                    Warrior.Warrior w = new Warrior.Warrior(Constant.Constant.defaultMinionATK, Constant.Constant.defaultMinionDEF, Constant.Constant.warriorMoveSpeed);
                    g.addWarrior(w);
                }
                break;
            }
        }
        List<Warrior.Warrior> wl = new List<Warrior.Warrior>();
        for (int i = 0; i < num; i++)
        {
            Warrior.Warrior w = new Warrior.Warrior(Constant.Constant.defaultMinionATK, Constant.Constant.defaultMinionDEF, Constant.Constant.warriorMoveSpeed);
            wl.Add(w);
        }
        Warrior.WarriorGroup newGroup = new Warrior.WarriorGroup(wl);
        this.warriors.Add(newGroup);
        this.totalWarriors += num;

    }

    private void checkAdjStars (MapElement.Star cur, float dist)
    {
        if (dist < 0)
        {
            return;
        }
        if (!this.viewedStars.Contains(cur))
        {
            this.viewedStars.Add(cur);
        }
        foreach (MapElement.Star star in cur.adj)
        {
            checkAdjStars(star, dist - Vector2.Distance(cur.pos, star.pos));
        }

    }

    //获取视野范围内星系
    public void addViewedStars ()
    {
        foreach (MapElement.Star star in this.ownedStars)
        {
            checkAdjStars(star, this.vision);
        }
    }

    public void sortViewedStars ()
    {

    }

    public void produceWarrior ()
    {
        int foodCost = this.totalWarriors * Constant.Constant.warriorMaintainenceCost;

    }

    //决定行动
    public void decideMoves()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
