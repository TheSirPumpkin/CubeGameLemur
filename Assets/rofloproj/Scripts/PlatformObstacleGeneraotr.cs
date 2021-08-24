using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObstacleGeneraotr : MonoBehaviour
{
    //public float ObstacleSpawnRate;
    public GameObject[] Obstacles;
    public int StartsOn;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var obstacle in Obstacles)
        {
            var cahnce = Random.Range(0, PlayerPrefs.GetInt("Level"));
            obstacle?.SetActive(false);
            if (cahnce > 0)
            {
                obstacle?.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
