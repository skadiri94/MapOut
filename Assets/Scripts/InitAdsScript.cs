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
    private bool shownAds = false;
    string bannerAdUnitId;
    string interstitialAdUnitId;
    string rewardAdUnitId;


    // Start is called before the first frame update
    void Start()
    {

        bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";

        rewardAdUnitId = "ca-app-pub-3940256099942544/5224354917";
        interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";

        

        MobileAds.Initialize(InitializationStatus => { });
        RequestBanner();

        //adMobReward = new RewardedAd(rewardAdUnitId);

        if (!shownAds)
        {


            //Google Interstitial
            requestInterstitial();
            if (adMobInterstitial.IsLoaded())
            {
                adMobInterstitial.Show();
                //countdownTime = 5f;
                //startTimer = true;
                //Time.timeScale = 1f;
                Debug.Log("Google Interstitial");
            }
            



            // if(startTimer && countdownTime >= 0.0f)
            // {
            //     countdownTime -= Time.deltaTime;
            // }

            if (adMobInterstitial != null && adMobInterstitial.IsLoaded())
            {
                adMobInterstitial.Destroy();
                //startTimer = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RequestBanner()
    {
#if UNITY_ANDROID

#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif


        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    //AdMob Interstitial
    private void requestInterstitial()
    {

        adMobInterstitial = new InterstitialAd(interstitialAdUnitId);

        AdRequest request = new AdRequest.Builder().Build();

        adMobInterstitial.LoadAd(request);

        shownAds = true;
    }



    //Google Rewarded
    public void requestRewarded()
    {
        adMobReward = new RewardedAd(rewardAdUnitId);

        AdRequest request = new AdRequest.Builder().Build();

        // Called when the user should be rewarded for interacting with the ad.
        adMobReward.OnUserEarnedReward += HandleUserEarnedReward;

        adMobReward.LoadAd(request);
    }

    public void showGoogleRewardedVideo()
    {
        requestRewarded();
        if (adMobReward.IsLoaded())
        {
            adMobReward.Show();
        }
    }


    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for 1 hint");
        //GetComponent<GameManager>().setHints(1);

    }

}
