using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeshift : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public PlayerMovement playerDeath;

    public bool timeShift = false;

    void Start()
    {
        timeShift = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (timeShift)
            {
                TimeShiftOff();
            }
            else if (playerDeath.alive == false)
            {
                TimeShiftOff();
            }
            else
            {
                TimeShiftOn();
            }
        }
    }
    void TimeShiftOff()
    {
        Time.timeScale = 1;
        if (pauseMenu.gamePause == true)
        {
            Time.timeScale = 0;
        }
        timeShift = false;
    }
    public void TimeShiftOn()
    {
        Time.timeScale = 0.5f;
        if (pauseMenu.gamePause == true)
        {
            Time.timeScale = 0;
        }
        timeShift = true;
    }
}