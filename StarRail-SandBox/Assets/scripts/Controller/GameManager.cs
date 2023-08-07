using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Galaxy;
using Star;
using Path;

public class GameManager : MonoBehaviour
{
    public List<Star.Star> stars = new List<Star.Star>();
    public List<GameObject> starprefabs = new List<GameObject>();
    public List<Path.Path> paths = new List<Path.Path>();
    public List<GameObject> pathprefabs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (Star.Star star in stars)
        {
            GameObject newstar = CreateGameObjectFromstar(star);
            starprefabs.Add(newstar);
        }  
        foreach (Path.Path path in paths)
        {
            GameObject newpath = CreateGameObjectFrompath(path);
            pathprefabs.Add(newpath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject CreateGameObjectFromstar(Star.Star star)
    {
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        return newGameObject;
    }

    private GameObject CreateGameObjectFrompath(Path.Path path)
    {
        GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        return newGameObject;
    }
}
