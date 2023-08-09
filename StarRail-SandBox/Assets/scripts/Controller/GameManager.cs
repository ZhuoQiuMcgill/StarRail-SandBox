using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Galaxy;
using Star;
using Path;

public class GameManager : MonoBehaviour
{
    public int width = 1000;
    public int height = 500;
    public int numStars = 100;

    private Galaxy.Galaxy map;
    //转录为GameObject list
    private List<GameObject> renderedStar = new List<GameObject>();
    private List<GameObject> renderedPath = new List<GameObject>();

    private Dictionary<GameObject, Star.Star> starDict = new Dictionary<GameObject, Star.Star>();

    public GameObject starPrefab;
    public GameObject livableStarPrefab;
    public GameObject blackholePrefab;
    public GameObject pathPrefab;
    public GameObject unionPathPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        // 创建地图
        Galaxy.Galaxy map = new Galaxy.Galaxy(this.numStars, this.width, this.height);
        this.map = map;

        CreateGameObject();
       
    }

    // Update is called once per frame
    void Update()
    {
        // 检查是否按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            // 转换鼠标位置到世界坐标
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 从鼠标位置发射一条射线
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // 如果射线击中了一个物体，并且物体的标签是"Star"
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Star"))
                {
                    StarData starData = hit.collider.gameObject.GetComponent<StarData>();
                    if (starData != null)
                    {
                        Debug.Log("Clicked on a Star: " + starData.id + " resources: \n" + starData.ResourcesInfo());
                    }
                    
                }
                else if (hit.collider.CompareTag("Path"))
                {
                    Debug.Log("Clicked on a Path: " + hit.collider.gameObject.name);
                }
                
            }
        }
    }


    // TODO: 将map里的星系以及路径生成成对应的gameobject
    private void CreateGameObject()
    {
        foreach (Star.Star star in this.map.stars)
        {          
            renderedStar.Add(CreateGameObjectFromStar(star));
        }

        foreach (Path.Path path in this.map.paths)
        {
            renderedStar.Add(CreateGameObjectFromPath(path));
        }
    }

    private GameObject CreateGameObjectFromPath(Path.Path path)
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
        rectangleObj.tag = "Path";

        // 设定位置和旋转
        rectangleObj.transform.position = new Vector3(midpoint.x, midpoint.y, -0.2f);
        rectangleObj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        // 设定大小
        rectangleObj.transform.localScale = new Vector3(length, 1, 1);

        // Debug.Log(rectangleName + ": " + path.distance);

        return rectangleObj;
    }

    private GameObject CreateGameObjectFromStar(Star.Star star)
    {
        Vector3 starPosition = new Vector3(star.pos.x, star.pos.y, -0.5f);
        GameObject prefab;

        if (star.type == 1) { prefab = this.blackholePrefab; }
        else
        {
            if (star.isLivable) { prefab = this.livableStarPrefab; }
            else { prefab = this.starPrefab; }
        }

        GameObject newStar = Instantiate(prefab, starPosition, Quaternion.identity);
        newStar.name = star.id.ToString();

        // 为新的星星GameObject设置标签
        newStar.tag = "Star";

        // 添加StarData组件并初始化数据
        StarData starData = newStar.AddComponent<StarData>();
        starData.Initialize(star);

        return newStar;
    }

}
