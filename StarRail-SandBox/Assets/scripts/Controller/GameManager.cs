using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MapElement;
using Rander;


public class GameManager : MonoBehaviour
{
    public int width = 1000;
    public int height = 500;
    public int numStars = 100;

    private MapElement.Galaxy map;
    private Rander.GraphRanderer randerer;
    
    private List<GameObject> resourcesStars = new List<GameObject>();
    private List<GameObject> livableStars = new List<GameObject>();
    private List<GameObject> blackholes = new List<GameObject>();

    private List<GameObject> renderedPath = new List<GameObject>();


    public GameObject starPrefab;
    public GameObject livableStarPrefab;
    public GameObject blackholePrefab;
    public GameObject pathPrefab;
    public GameObject unionPathPrefab;

    // UI组件
    public TMP_Text starResourcesText;
    public TMP_Text gameObjectUIText;

    public Material voronoiMaterial;

    // Start is called before the first frame update
    void Start()
    {

        // 创建地图
        MapElement.Galaxy map = new MapElement.Galaxy(this.numStars, this.width, this.height);
        this.map = map;

        Rander.GraphRanderer randerer = new Rander.GraphRanderer(this.map, this.voronoiMaterial);
        this.randerer = randerer;

        CreateGameObject();
        Debug.Log("Resources Stars: " + this.resourcesStars.Count);
        Debug.Log("Livable Stars: " + this.livableStars.Count);
        Debug.Log("Blackhole: " + this.blackholes.Count);


    }

    // Update is called once per frame
    void Update()
    {
        // 检查是否按下鼠标左键
        if (Input.GetMouseButtonDown(0)) { mouseLeftClickAction(); }
        
    }


    /**
     * 鼠标左键函数
     */
    private void mouseLeftClickAction()
    {
        // 转换鼠标位置到世界坐标
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 从鼠标位置发射一条射线
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            // 鼠标击中星系
            if (hit.collider.CompareTag("Star"))
            {
                StarData starData = hit.collider.gameObject.GetComponent<StarData>();
                if (starData != null)
                {
                    Debug.Log("Clicked on a Star: " + starData.star.id + " resources: \n" + starData.ResourcesInfo());
                    starResourcesText.text = starData.ResourcesInfo();
                    gameObjectUIText.text = "Star: " + hit.collider.gameObject.name;
                }

            }

            // 鼠标击中路径
            else if (hit.collider.CompareTag("Path"))
            {
                PathData pathData = hit.collider.gameObject.GetComponent<PathData>();
                Debug.Log("Clicked on a Path: " + hit.collider.gameObject.name + "\tspeed rate: " + pathData.path.speedRate);
                starResourcesText.text = "";
                gameObjectUIText.text = "Path: " + hit.collider.gameObject.name;
            }

        }
        else { 
            starResourcesText.text = "";
            gameObjectUIText.text = "";
        }
    }


    // TODO: 将map里的星系以及路径生成成对应的gameobject
    private void CreateGameObject()
    {
        foreach (MapElement.Star star in this.map.stars)
        {          
            if (star.type == 1) { this.blackholes.Add(CreateGameObjectFromStar(star)); }
            else
            {
                if (star.isLivable) { this.livableStars.Add(CreateGameObjectFromStar(star)); }
                else { this.resourcesStars.Add(CreateGameObjectFromStar(star)); }
            }
        }

        foreach (MapElement.Path path in this.map.paths)
        {
            renderedPath.Add(CreateGameObjectFromPath(path));
        }
    }

    private GameObject CreateGameObjectFromPath(MapElement.Path path)
    {
        Vector2 pos1 = path.star1.pos;
        Vector2 pos2 = path.star2.pos;

        // 计算中点
        Vector2 midpoint = (pos1 + pos2) / 2;

        // 计算长度
        float length = Vector2.Distance(pos1, pos2);

        // 计算旋转角度
        float angleRad = Mathf.Atan2(pos2.y - pos1.y, pos2.x - pos1.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // 实例化预制体
        GameObject rectangleObj = Instantiate(path.unionPath ? unionPathPrefab : pathPrefab);

        // 设定名字
        string rectangleName = $"{path.star1.id}-{path.star2.id}";
        rectangleObj.name = rectangleName;

        // 添加tag
        rectangleObj.tag = "Path";

        // 设定父级
        Transform parent = GameObject.Find("Map/Paths").transform;
        rectangleObj.transform.SetParent(parent);

        // 添加PathData组件并初始化数据
        PathData pathData = rectangleObj.AddComponent<PathData>();
        pathData.Initialize(path);

        // 设定位置和旋转
        rectangleObj.transform.position = new Vector3(midpoint.x, midpoint.y, -0.2f);
        rectangleObj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        // 设定大小
        rectangleObj.transform.localScale = new Vector3(length, 1, 1);

        return rectangleObj;
    }

    private GameObject CreateGameObjectFromStar(MapElement.Star star)
    {
        Vector3 starPosition = new Vector3(star.pos.x, star.pos.y, -0.5f);
        GameObject prefab;
        Transform parent;

        // 分类设定渲染物以及父级
        if (star.type == 1) 
        { 
            prefab = this.blackholePrefab;
            parent = GameObject.Find("Map/Stars/Blackhole").transform;
        }
        else
        {
            if (star.isLivable) 
            { 
                prefab = this.livableStarPrefab;
                parent = GameObject.Find("Map/Stars/LivableStars").transform;
            }
            else 
            { 
                prefab = this.starPrefab;
                parent = GameObject.Find("Map/Stars/ResourcesStars").transform;
            }
        }


        GameObject newStar = Instantiate(prefab, starPosition, Quaternion.identity, parent);
        newStar.name = star.id.ToString();

        // 为新的星星GameObject设置标签
        newStar.tag = "Star";

        // 添加StarData组件并初始化数据
        StarData starData = newStar.AddComponent<StarData>();
        starData.Initialize(star);

        return newStar;
    }

}
