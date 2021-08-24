using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RespawnController : MonoBehaviour
{
    public Transform LastBridge;
    public GameObject RespawnPoUp;
    public GameObject DeathWall;
    public GameObject Canvas;
    public VideoPlayer Video;
    public GameObject RewardButton;
    private GameManager GetGameManager;
    private PlayerMovement player;
    private bool endGameEnabled;

    private void Start()
    {
        GetGameManager = GetComponent<GameManager>();
    }

 
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.FirstDeath = true;

        Canvas.SetActive(true);
        Canvas.GetComponent<PauseMenu>().PreGame();
        
        GetGameManager.JoystickMove.rb[0].gameObject.SetActive(true);
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        HandleFailedAd();
    }
    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        HandleFailedAd();
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        HandleFailedAd();
    }

    public void EnableRespawnPopup(PlayerMovement player)
    {
        this.player = player;

        endGameEnabled = true;

        RespawnPoUp.SetActive(true);
        SetupTexts(RespawnPoUp);


        // GoogleMobileAdsManager.DeathCount++;
        // if (GoogleMobileAdsManager.DeathCount >= 2)
        // {
        //ShowInterstitial();
        // }
        Time.timeScale = 0;
    }

    public void DisableRespawnPopup()
    {
        Time.timeScale = 1;
        RespawnPoUp.SetActive(false);
    }
    public void DeclineBoost()
    {
        DisableRespawnPopup();
        GetGameManager.JoystickMove.rb.Clear();
        player.Die();

    }
    public void PlayRewardVideo()
    {
        RespawnPoUp.SetActive(false);

        Time.timeScale = 0;
        GetGameManager.JoystickMove.rb[0].transform.position += new Vector3(0f, 10f, 0f);
        RespawnPoUp.SetActive(false);


        if (LastBridge != null)
        {
            GetGameManager.JoystickMove.rb[0].transform.position = new Vector3(LastBridge.transform.position.x + 3f, 1.997f, 0f);
            if (Mathf.Abs(GetGameManager.JoystickMove.rb[0].transform.position.x - DeathWall.transform.position.x) < 10)
            {
                DeathWall.transform.position = GetGameManager.JoystickMove.rb[0].transform.position - new Vector3(8 + PlayerPrefs.GetInt("Level"), 1.997f, 0);
            }
        }
        else
        {
            GetGameManager.JoystickMove.rb[0].transform.position = new Vector3(0f, 1.997f, 0f);
            DeathWall.transform.position = GetGameManager.JoystickMove.rb[0].transform.position - new Vector3(8 + PlayerPrefs.GetInt("Level"), 1.997f, 0);
        }

        GoogleMobileAdsManager.Instance.ShowRewarded();
    }
    private void HandleFailedAd()
    {
        if (!endGameEnabled)
        {
            return;
        }
       // Canvas.GetComponent<PauseMenu>().Pause();
       // Canvas.GetComponent<PauseMenu>().Resume();

        RespawnPoUp.SetActive(true);
        RewardButton.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Invoke("PauseAfterPromo", 0.1f);
    }
    private void PauseAfterPromo()
    {
        Debug.Log("HandleOnAdClosed");
        Time.timeScale = 0;
    }
    //private void ShowInterstitial()
    //{
    //    if (RewardButton.gameObject.activeSelf == false)
    //    {
    //        GoogleMobileAdsManager.Instance.ShowInterstitial();
    //        //GoogleMobileAdsManager.DeathCount = 0;
    //    }
    //}

    private void SetupTexts(GameObject popup)
    {
        var texts = popup.GetComponent<EndGameUiPopup>();
        texts.LevelTxt.text = $"Level- { PlayerPrefs.GetInt("LevelUi")}";
        texts.PointsTxt.text = $"Points:{ PointManager.Instance.CollectedPoints}";
    }
}
