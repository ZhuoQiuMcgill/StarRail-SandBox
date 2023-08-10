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
    

    private float aggressivity = 0.4f;

    private int populationWeight = 0;

    //资源权重
    private int metalWeight = 0;
    private int energyWeight = 0;
    private int foodWeight = 0;

    //视野范围
    private int vision = 150;

    public void createWarriors (MapElement.Star star, int num)
    {
        foreach (Warrior.WarriorGroup g in this.warriors) 
        {
            if (g.star.id == star.id)
            {
                for (int i = 0; i < num; i++)
                {
                    Warrior.Warrior w = new Warrior.Warrior(Constant.Constant.defaultMinionATK, Constant.Constant.defaultMinionDEF, Constant.Constant.WarriorMoveSpeed);
                    g.addWarrior(w);
                }
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    Warrior.Warrior w = new Warrior.Warrior(Constant.Constant.defaultMinionATK, Constant.Constant.defaultMinionDEF, Constant.Constant.WarriorMoveSpeed);
                    List<Warrior.Warrior> wl = new List<Warrior.Warrior>();
                    Warrior.WarriorGroup newGroup = new Warrior.WarriorGroup(wl);
                    this.warriors.Add(newGroup);
                }
            }
        }
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
