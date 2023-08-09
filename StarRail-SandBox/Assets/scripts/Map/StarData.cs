using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Star;
using MapResources;
using System.Text;


public class StarData : MonoBehaviour
{
    public int id;
    public int type;
    public Vector2 pos;
    public bool isLivable;
    public bool isDestroyed;
    public List<Star.Star> adjStars;
    public Dictionary<MapResources.Resources, int> resources;
    public int resTick;    
    public int maxResValue;   
    public int minResValue;    

    public void Initialize(Star.Star star)
    {
        id = star.id;
        type = star.type;
        pos = star.pos;
        isLivable = star.isLivable;
        isDestroyed = star.isDestroyed;
        adjStars = star.adj;
        resources = star.resources;
        resTick = star.resTick;
        maxResValue = star.maxResValue;
        minResValue = star.minResValue;

    }

    public string ResourcesInfo()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var pair in this.resources)
        {
            sb.AppendLine($"{pair.Key.ToCustomString()}: {pair.Value}");
        }

        return sb.ToString();
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
