using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleMobileAdsManager : MonoBehaviour
{
    // Create const fields for those:
    //ca-app-pub-3828633425506712~6549182311 android Id
    //ca-app-pub-3828633425506712/8241801261 banner Id
    //ca-app-pub-3828633425506712/7891281623 interstitial Id
    //ca-app-pub-3828633425506712/4881974902 rewarded Id

    public static GoogleMobileAdsManager Instance;
    public RespawnController RespawnController;
    public RewardedAd RewardedAd { get; private set; }

    private BannerView bannerView;
    private InterstitialAd interstitial;
  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
        RequestInterstitial();
        RequestRewardedAd();
    }
    private void OnDestroy()
    {
        UnSubscribe();
    }
    public void UnSubscribe()
    {
        this.interstitial.OnAdClosed -= RespawnController.HandleOnAdClosed;
        this.RewardedAd.OnUserEarnedReward -= RespawnController.HandleUserEarnedReward;
        this.RewardedAd.OnAdFailedToLoad -= RespawnController.HandleRewardedAdFailedToLoad;
        this.RewardedAd.OnAdFailedToShow -= RespawnController.HandleRewardedAdFailedToShow;
        this.RewardedAd.OnAdClosed -= RespawnController.HandleRewardedAdClosed;
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            RequestInterstitial();
            this.interstitial.Show();
        }
    }

    public void ShowRewarded()
    {
        if (this.RewardedAd.IsLoaded())
        {
            this.RewardedAd.Show();
        }
        //else
        //{
        //    RequestRewardedAd();
        //    this.rewardedAd.Show();
        //}
    }

    private void RequestRewardedAd()
    {
        this.RewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");// TEST ID

        AdRequest request = new AdRequest.Builder().Build();

        this.RewardedAd.LoadAd(request);

        this.RewardedAd.OnUserEarnedReward += RespawnController.HandleUserEarnedReward;
        this.RewardedAd.OnAdFailedToLoad += RespawnController.HandleRewardedAdFailedToLoad;
        this.RewardedAd.OnAdFailedToShow += RespawnController.HandleRewardedAdFailedToShow;
       // this.RewardedAd.OnAdClosed += RespawnController.HandleRewardedAdClosed;
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";// TEST ID

        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }

    private void CloseBannerAd()
    {
        bannerView.Destroy();
    }

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";// TEST ID

        this.interstitial = new InterstitialAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);

        this.interstitial.OnAdClosed += GameObject.FindObjectOfType<RespawnController>().HandleOnAdClosed;

    }


}
