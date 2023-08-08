using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Galaxy;
using Star;
using Path;

public class GameManager : MonoBehaviour
{
    public int width = 10000;
    public int height = 5000;
    public int numStars = 1000;

    private Galaxy.Galaxy map;


    // Start is called before the first frame update
    void Start()
    {
        // ������ͼ
        Galaxy.Galaxy map = new Galaxy.Galaxy(this.numStars, this.width, this.height);
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

    }

    private GameObject PathToRect(Path path)
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

        // ���һ��ͼ���������������ɫ
        var imageComponent = rectangleObj.AddComponent<Image>();
        imageComponent.color = Color.white;

        return rectangleObj;
    }

    // TODO: ���Ѿ�������gameobject��Ⱦ������
    private void rander()
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
