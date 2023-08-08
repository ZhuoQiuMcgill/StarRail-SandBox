using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Galaxy;

public class GalaxyRanderer : MonoBehaviour
{
    //public int width = 10000;
    //public int height = 5000;
    //public GameObject[] star;
    //public GameObject[] path;
    //public int starCount = 2500;
    //public Vector3 spawnArea = new Vector3(10000, 5000,0);


    // Start is called before the first frame update
    void Start()
    {
        //GenerateMap();
        //Galaxy.Galaxy galaxy = new Galaxy.Galaxy(2500, 10000, 5000);
        //galaxy.GenerateStarsAndPaths();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void GenerateMap()
    //{
    //    for (int i = 0; i < starCount; i++)
    //    {
    //        // 选择一个Prefab
    //        GameObject starPrefab = star[Random.Range(0, star.Length)];
    //
    //        // 随机生成位置
    //        float randomX = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
    //        float randomY = Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f);
    //        Vector3 spawnPosition = new Vector3(randomX, randomY, -0.1f) + transform.position;
    //
    //        // 实例化Prefab
    //        Instantiate(starPrefab, spawnPosition, Quaternion.identity);
    //    }
    //}
}
