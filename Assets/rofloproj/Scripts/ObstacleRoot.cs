using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRoot : MonoBehaviour
{
    public GameObject[] Obstacles;
  
    void Start()
    {
        foreach (var obstacle in Obstacles)
        {
            obstacle.SetActive(false);
        }
        Obstacles[Random.RandomRange(0, Obstacles.Length)].SetActive(true);
    }
}
