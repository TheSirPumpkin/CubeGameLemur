using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    public int scoreBestValue;
    Text scoreBest;
    void Start()
    {
       // scoreBest = GetComponent<Text>();
    }

    void Update()
    {
        //scoreBest.text = "BEST SCORE: \n" + PlayerPrefs.GetInt("BestScore");
    }
}