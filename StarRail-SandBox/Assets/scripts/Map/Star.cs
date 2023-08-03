using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapResources; 

public class Star : MonoBehaviour
{
    public int id { get; }
    public int type { get; set; }
    public Vector2 pos { get; set; }
    public bool isLivable = false;
    public Dictionary<MapResources.Resources, int> resources = new Dictionary<MapResources.Resources, int>()
            {
                {MapResources.Resources.Population, 0},
                {MapResources.Resources.Food, 0},
                {MapResources.Resources.Metal, 0},
                {MapResources.Resources.Energy, 0},
                {MapResources.Resources.Technology, 0}
            };

    public Star(int id, Vector2 pos)
    {   
        this.id = id;
        this.pos = pos;
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
