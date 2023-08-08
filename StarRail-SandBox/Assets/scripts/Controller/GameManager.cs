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

    public GameObject starprefab;
    public GameObject pathprefab;

    // Start is called before the first frame update
    void Start()
    {
        // 创建地图
        Galaxy.Galaxy map = new Galaxy.Galaxy(this.numStars, this.width, this.height);
        Debug.Log(map.stars.Count);
        this.map = map;
        CreateGameObject();
        rander();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: 将map里的星系以及路径生成成对应的gameobject
    private void CreateGameObject()
    {
        foreach (Star.Star star in this.map.stars)
        {
            GameObject newStar = CreateGameObjectFromstar(star);
            renderedStar.Add(newStar);
        }

        foreach (Path.Path path in this.map.paths)
        {
            GameObject newPath = CreateGameObjectFrompath(path);
            renderedStar.Add(newPath);
        }
    }

    private GameObject CreateGameObjectFrompath(Path.Path path)
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

        // 创建GameObject，并以两个星系的ID为名
        string rectangleName = $"{path.star1.id}-{path.star2.id}";
        GameObject rectangleObj = new GameObject(rectangleName);
        var rectTransform = rectangleObj.AddComponent<RectTransform>();

        // 假设宽度为0.5，可以根据需要更改
        float width = 0.5f;

        rectTransform.sizeDelta = new Vector2(length, width);
        rectTransform.position = midpoint;
        rectTransform.rotation = Quaternion.Euler(0, 0, angleDeg);


        return rectangleObj;
    }

    private GameObject CreateGameObjectFromstar(Star.Star star)
    {
        Vector3 starPosition = new Vector3(star.pos.x, star.pos.y, -0.2f);
        GameObject newStar = Instantiate(starprefab, starPosition, Quaternion.identity);
        newStar.name = star.id.ToString();
        newStar.layer = star.type;
        newStar.isStatic = star.isLivable;
        newStar.isStatic = star.isDestroyed;
        return newStar;
    }

    // TODO: 将已经生产的gameobject渲染至画面
    private void rander()
    {
        foreach (GameObject newStar in renderedStar)
        {
            Instantiate(starprefab, newStar.transform.position, Quaternion.identity);            
        }
        foreach (GameObject newPath in renderedPath)
        {
            Instantiate(pathprefab, new Vector3(newPath.transform.position.x, newPath.transform.position.y, -0.2f), Quaternion.identity);
        }
    }
}
