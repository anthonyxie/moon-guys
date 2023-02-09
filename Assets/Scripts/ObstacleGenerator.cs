using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obstacleFab;
    public GameObject bigFab;
    public GameObject collectableFab;
    public GameObject gameHandler;
    private int difficulty;
    private float[,] locations1 = new float[,] {{0f,0f}};
    private float[,] locations2 = new float[,] {{-2f,2f},{2f,2f},{4f,0f},{0f,0f},{-4f,0f}, {2f,-2f}, {-2f,-2f}};
    private float[,] locations3 = new float[,] {{-4f, 3f}, {0f,3f}, {4f, 3f}, {-4f,0f},{0f, 0f}, {4f,0f},{-4f, -3f}, {0f,-3f}, {4f,-3f}};
    private float[,] collections1 = new float[16,2];
    private float[,] collections2 = new float[,] {{0f,1f},{-1f,-1f},{1f,-1f}};
    private float[,] collections3 = new float[8,2];
    private float[,] locations;
    private float[,] collections;
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty", 0);
        setCollections1();
        setCollections3();
        generateObstacles();
        generateCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCollections1() {
        for (int i = 0; i < collections1.GetLength(0); i++) {
            float ang = ((22.5f * (float)i) * MathF.PI) / 180f;
            collections1[i,0] = 2.5f * MathF.Cos(ang);
            collections1[i,1] = 2.5f * MathF.Sin(ang);
        }
    }

    void setCollections3() {
        for (int i = 0; i < collections3.GetLength(0); i++) {
            float ang = ((45f * (float)i) * MathF.PI) / 180f;
            collections3[i,0] = MathF.Cos(ang);
            collections3[i,1] = MathF.Sin(ang);
        }
    }
    void generateObstacles() {
        switch (difficulty) {
            case 0:
                locations = locations1;
                collections = collections1;
                obstacleFab = bigFab;
                break;
            case 1:
                locations = locations2;
                collections = collections2;
                break;
            case 2:
                locations = locations3;
                collections = collections3;
                break;
        }

        for (int i = 0; i < locations.GetLength(0); i++) {
            GameObject obs = Instantiate(obstacleFab);
            obs.name = $"Obstacle{i}";
            obs.transform.position = new Vector3(locations[i,0], locations[i,1], 0);
            for (int j = 0; j < collections.GetLength(0); j++) {
                GameObject col = Instantiate(collectableFab);
                col.transform.position = new Vector3(locations[i,0] + collections[j,0], locations[i,1] + collections[j,1], 0);
            }
        }
    }

    void generateCollectables() {

    }
}
