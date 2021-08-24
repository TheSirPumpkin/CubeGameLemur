﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    public float MoveSpeed;
    public PauseMenu PauseMenu;
    private bool startLevel;
    private void OnEnable()
    {
        PauseMenu.ResumeGameEvent.AddListener(Run);
    }
    private void OnDisable()
    {
        PauseMenu.ResumeGameEvent.RemoveListener(Run);
    }
    private void Run()
    {
        StartCoroutine(StartChase());
    }
    IEnumerator StartChase()
    {
        yield return new WaitForSeconds(PlayerPrefs.GetInt("Level") / 2);
        startLevel = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startLevel)
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed * PlayerPrefs.GetInt("Level")/2);
        }
    }
}