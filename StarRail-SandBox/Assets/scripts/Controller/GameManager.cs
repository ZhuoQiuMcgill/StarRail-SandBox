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
    public GameObject unionpathprefab;

    // Start is called before the first frame update
    void Start()
    {
        // �鿴pathprefab�Ƿ���ȷ��
        if (pathprefab == null)
        {
            Debug.LogError("pathPrefab is null!");
        }
        else
        {
            Debug.Log("pathPrefab is correctly assigned.");
        }

        // ������ͼ
        Debug.Log("Before Map Create");
        Galaxy.Galaxy map = new Galaxy.Galaxy(this.numStars, this.width, this.height);
        Debug.Log("Map create successed");
        Debug.Log(map.paths.Count);
        this.map = map;
        CreateGameObject();
        //rander();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(this.map.stars.Count);
    }

    // TODO: ��map�����ϵ�Լ�·�����ɳɶ�Ӧ��gameobject
    private void CreateGameObject()
    {
        foreach (Star.Star star in this.map.stars)
        {          
            renderedStar.Add(CreateGameObjectFromstar(star));
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

        // �����е�
        Vector2 midpoint = (pos1 + pos2) / 2;

        // ���㳤��
        float length = Vector2.Distance(pos1, pos2);

        // ������ת�Ƕ�
        float angleRad = Mathf.Atan2(pos2.y - pos1.y, pos2.x - pos1.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // ʵ����Ԥ����
        GameObject rectangleObj = Instantiate(path.unionPath ? unionpathprefab : pathprefab);

        // �趨����
        string rectangleName = $"{path.star1.id}-{path.star2.id}";
        rectangleObj.name = rectangleName;

        // �趨λ�ú���ת
        rectangleObj.transform.position = new Vector3(midpoint.x, midpoint.y, -0.2f);
        rectangleObj.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        // �趨��С
        rectangleObj.transform.localScale = new Vector3(length, 1, 1);

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
            Instantiate(pathprefab, newPath.transform.position, Quaternion.identity);
        }
    }
}
