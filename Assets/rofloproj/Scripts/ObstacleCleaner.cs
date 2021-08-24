using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCleaner : MonoBehaviour
{
    public ObstacleRoot ObstacleRoot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ground>())
        {
            foreach (var part in ObstacleRoot.Obstacles)
            {
                if (part.activeSelf)
                {
                    part.GetComponent<ObstaclePart>().DestroyOnGrow();
                }
            }
        }
    }
}
