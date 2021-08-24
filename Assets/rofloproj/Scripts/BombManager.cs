using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    public GameObject[] bombs;
    public GameObject attention;

    void Start()
    {
        //attention.SetActive(false);
        //bombs[0].SetActive(false);
        //bombs[1].SetActive(false);
        //bombs[2].SetActive(false);
    }

    void Update()
    {
        //if (scoreCounter.scoreValue == 1)
        //{
        //    attention.SetActive(true);
        //    StartCoroutine(BombSpawnDelay());
        //    bombs[0].SetActive(true);
        //}
        //if (scoreCounter.scoreValue == 10)
        //{
        //    attention.SetActive(true);
        //    StartCoroutine(BombSpawnDelay());
        //    bombs[1].SetActive(true);
        //}
        //if (scoreCounter.scoreValue == 25)
        //{
        //    attention.SetActive(true);
        //    StartCoroutine(BombSpawnDelay());
        //    bombs[2].SetActive(true);
        //}
    }
    public IEnumerator BombSpawnDelay()
    {
        yield return new WaitForSeconds(2);
        attention.SetActive(false);
    }

    public void SpawnNewBomb()
    {
        Instantiate(bombs[0],transform.position,Quaternion.identity);
    }
}