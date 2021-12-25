using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent ResumeGameEvent;
    public PlayerMovement playerDeath;
    public Timeshift timeshift;
    public JoystickMove JoystickMove;
    public bool gamePause = false;
    public GameObject pausePanel;
    public GameObject joystick;
    public GameObject StartGamePanel;
    public GameObject GamePanel;
    private bool resumePressed;
    private bool preGameDisabled;

    void Awake()
    {
        playerDeath.alive = true;
        pausePanel.SetActive(false);
    }
    private void Start()
    {
        PreGame();
        //Invoke("PreGame", 0.01f);
    }
    public void ShowHighScore()
    {
        Social.ShowLeaderboardUI();
    }
    private void OnApplicationFocus(bool focus)
    {
        if (!focus && Time.timeScale != 0 && !gamePause && preGameDisabled)
        {
            Debug.Log("OnApplicationPause " + gamePause + " " + Time.timeScale);
            Pause();
        }
    }
    public void Resume()
    {
        if (!resumePressed)
        {
            resumePressed = true;
            JoystickMove.ResetMove();
            ResumeGameEvent.Invoke();
            joystick.SetActive(true);
            pausePanel.SetActive(false);
            gamePause = false;
            Time.timeScale = 1;
            if (playerDeath.alive == false)
            {
                joystick.SetActive(false);
            }
            JoystickMove.rb[0].isKinematic = true;
            JoystickMove.rb[0].isKinematic = false;
            joystick.GetComponent<DynamicJoystick>().HandleInput(0, Vector2.zero, Vector2.zero, null);

            //GoogleMobileAdsManager.Instance.CloseBannerAd();
        }
    }
    public void Pause()
    {
        resumePressed = false;
        joystick.SetActive(false);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        gamePause = true;

        //GoogleMobileAdsManager.Instance.RequestBanner();
    }
    public void PreGame()
    {
        resumePressed = false;
        joystick.SetActive(false);
        Time.timeScale = 0;
        StartGamePanel.SetActive(true);
        GamePanel.SetActive(false);
        gamePause = true;
        preGameDisabled = true;

        //GoogleMobileAdsManager.Instance.RequestBanner();
    }


    public void StartNextLevel()
    {
        //Application.LoadLevelAsync(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}