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
    //ת¼ΪGameObject list
    private List<GameObject> renderedStar = new List<GameObject>();
    private List<GameObject> renderedPath = new List<GameObject>();

    public GameObject starprefab;
    public GameObject pathprefab;

    // Start is called before the first frame update
    void Start()
    {
        // ������ͼ
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

    // TODO: ��map�����ϵ�Լ�·�����ɳɶ�Ӧ��gameobject
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

        // �����е�
        Vector2 midpoint = (pos1 + pos2) / 2;

        // ���㳤��
        float length = Vector2.Distance(pos1, pos2);

        // ������ת�Ƕ�
        float angleRad = Mathf.Atan2(pos2.y - pos1.y, pos2.x - pos1.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // ����GameObject������������ϵ��IDΪ��
        string rectangleName = $"{path.star1.id}-{path.star2.id}";
        GameObject rectangleObj = new GameObject(rectangleName);
        var rectTransform = rectangleObj.AddComponent<RectTransform>();

        // ������Ϊ0.5�����Ը�����Ҫ����
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

    // TODO: ���Ѿ�������gameobject��Ⱦ������
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
