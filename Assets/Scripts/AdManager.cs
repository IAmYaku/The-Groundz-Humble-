using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudienceNetwork;
using tv.superawesome.sdk.publisher;
using System;

public class AdManager : MonoBehaviour
{
    private static bool isFaceBookAd = false;
    private static bool isSteamAd = false;
    public static bool isSuperAwesome = true;

   // public SteamManager steamManager;
   private FacebookManager facebookManager;

    private SABannerAd banner = null;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        LoadSteam();

        LoadFacebook();
       
        LoadSuperAwesome();
    }

    private void LoadSteam()
    {
        // steamManager = GetComponent<SteamManager>();


        if (isSteamAd)
        {
            //   steamManager.enabled = true;
        }
        else
        {
            // steamManager.enabled = false;
        }
    }

    private void LoadFacebook()
    {
        facebookManager = GetComponent<FacebookManager>();

        if (isFaceBookAd)
        {
            facebookManager.enabled = true;
        }
        else
        {
            facebookManager.enabled = false;
        }
    }

    private void LoadSuperAwesome()
    {
        if (isSuperAwesome)
        {
            AwesomeAds.init(true);
        }
    }

    public void LoadInterstitialAd()
    {
        SAInterstitialAd.setConfigurationProduction();

        // to display test ads

        // lock orientation to portrait or landscape
        SAInterstitialAd.setOrientationPortrait();

        // enable or disable the android back button
        SAInterstitialAd.enableBackButton();

        // start loading ad data for a placement
        SAInterstitialAd.load(43105);

        playInterstitial();
        // LoadBanner("SuperAwesome");
    }

    public void playInterstitial()
    {

        // check if ad is loaded
        if (SAInterstitialAd.hasAdAvailable(43105))
        {
            print("Interstitial Ad Loaded");
            // display the ad
            SAInterstitialAd.play(43105);
        }
    }

    public void LoadBanner(string type)
    {
        if (type == "SuperAwesome")
        {
            // create a new banner
            banner = SABannerAd.createInstance();

            // set configuration production
            banner.setConfigurationProduction();

            // to display test ads
           // banner.enableTestMode();

            // start loading ad data for a placement
            banner.load(43105);

            // check if ad is loaded
            if (banner.hasAdAvailable())
            {
                print("Banner Loaded");
                // set a size template
                banner.setSize_320_50();

                // set a background color
                banner.setColorGray();

                // choose between top or bottom
                banner.setPositionTop();

                // display the ad
                banner.play();
            }
        }

    }

    public void LoadsSuperAwesomeVideo() {

        // make whole video surface clickable
        SAVideoAd.disableSmallClickButton();

        // set config production
        SAVideoAd.setConfigurationProduction();

        // to display test ads

        // lock orientation to portrait or landscape
        SAVideoAd.setOrientationPortrait();

        // enable or disable the android back button
        SAVideoAd.enableBackButton();

        // enable or disable a close button
        SAVideoAd.enableCloseButton();

        // enable or disable auto-closing at the end
        SAVideoAd.disableCloseAtEnd();

        // start loading ad data for a placement
        SAVideoAd.load(43304);
    }

    public void PlaySuperAwesomeVideo()
    {

        // check if ad is loaded
        if (SAVideoAd.hasAdAvailable(43304))
        {
           print("Video Ad Loaded"); 

            // display the ad
            SAVideoAd.play(43304);
        }
    }
}
