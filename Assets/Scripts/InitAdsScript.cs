using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InitAdsScript : MonoBehaviour
{

    //AdMob
    private InterstitialAd adMobInterstitial;
    private RewardedAd adMobReward;
    BannerView bannerView;
    string interstitialAdUnitId;
    string rewardAdUnitId;


    static InitAdsScript instance = null;
    public static InitAdsScript Instance
    {
        get
        {

            if (instance == null)
            {

                instance = GameObject.FindObjectOfType(typeof(InitAdsScript)) as InitAdsScript;
            }

            return instance;
        }
    }
  
    // Start is called before the first frame update
    void Start()
    {

        

        MobileAds.Initialize(initStatus => { });


        this.RequestBanner();
        this.requestInterstitial();
        this.requestRewarded();
        

    }

  
    void Update()
    {
      

    }

    private void RequestBanner()
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnitId = "unexpected_platform";
#endif

        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }

    //AdMob Interstitial
    public void requestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        // Initialize an InterstitialAd.
        this.adMobInterstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.adMobInterstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.adMobInterstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.adMobInterstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.adMobInterstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.adMobInterstitial.LoadAd(request);

    }


    public void ShowInterstitial()
    {

        if (adMobInterstitial.IsLoaded())
        {
            adMobInterstitial.Show();
        }
        else
            requestInterstitial();

    }



    //Google Rewarded
    public void requestRewarded()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        this.adMobReward = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.adMobReward.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.adMobReward.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.adMobReward.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.adMobReward.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.adMobReward.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.adMobReward.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.adMobReward.LoadAd(request);
    }

    public void showGoogleRewardedVideo()
    {

        if (adMobReward.IsLoaded())
        {
            adMobReward.Show();

        }
        else requestRewarded();
    }


    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }


    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.LoadAdError.GetMessage());
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.AdError.GetMessage());
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        QuestionsManager.Instance.setHints(1);
    }



}
