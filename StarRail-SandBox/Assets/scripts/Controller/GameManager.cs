using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Galaxy;
using Star;
using Path;

public class GameManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public List<Star.Star> stars = new List<Star.Star>();
    public List<GameObject> starprefabs = new List<GameObject>();
    public List<Path.Path> paths = new List<Path.Path>();
    public List<GameObject> pathprefabs = new List<GameObject>();
=======
    public int width = 1000;
    public int height = 500;
    public int numStars = 100;

    private Galaxy.Galaxy map;
>>>>>>> Stashed changes

    //转录为GameObject list
    private List<GameObject> renderedStar = new List<GameObject>();
    private List<GameObject> renderedPath = new List<GameObject>();

    public GameObject starprefab;
    public GameObject pathprefab;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
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
=======
        // 创建地图
        Galaxy.Galaxy map = new Galaxy.Galaxy(this.numStars, this.width, this.height);
        this.map = map;
        CreateGameObject();
        rander();
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
    }

<<<<<<< Updated upstream
=======
    // TODO: 将map里的星系以及路径生成成对应的gameobject
    private void CreateGameObject()
    {
        foreach(Star.Star star in this.map.stars)
        {
            Vector3 starPosition = new Vector3(star.pos.x, star.pos.y, 0f);
            GameObject newStar = Instantiate(starprefab, starPosition, Quaternion.identity);
            newStar.name = star.id.ToString();
            newStar.layer = star.type;
            newStar.isStatic = star.isLivable;
            newStar.isStatic = star.isDestroyed;
            renderedStar.Add(newStar);
        }
    }

    // TODO: 将已经生产的gameobject渲染至画面
    private void rander()
    {

    }

>>>>>>> Stashed changes
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
