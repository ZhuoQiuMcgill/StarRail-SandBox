using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Galaxy;
using Constant;
using Emanator;
using Warrior;

public class AITemplate : MonoBehaviour
{
    private List<Star.Star> ownedStars { get; } = new List<Star.Star>();
    private List<Emanator.Emanator> emanators = new List<Emanator.Emanator>();
    private List<Warrior.Warrior> warriors = new List<Warrior.Warrior>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
