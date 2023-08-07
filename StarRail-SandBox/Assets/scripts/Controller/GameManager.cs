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
        // 创建地图
        Galaxy.Galaxy map = new Galaxy.Galaxy(this.numStars, this.width, this.height);
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

    }

    // TODO: 将已经生产的gameobject渲染至画面
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
