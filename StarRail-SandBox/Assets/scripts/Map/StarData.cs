using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapElement;
using System.Text;


public class StarData : MonoBehaviour
{
    public MapElement.Star star;

    public void Initialize(MapElement.Star star)
    {  
        this.star = star;
    }

    public string ResourcesInfo()
    {
        StringBuilder sb = new StringBuilder();

        foreach (var pair in this.star.resources)
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
