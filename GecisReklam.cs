using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GecisReklam : MonoBehaviour
{
    private InterstitialAd InterGecisReklami;
    private InterstitialAd InterGecisVideoReklami;
    public string AndroidBannerReklamKimligi;
    public string �osBannerReklamKimligi;
    public string VideoAndroidBannerReklamKimligi;
    public string Video�osBannerReklamKimligi;
    string Reklamid;
    string Reklamid2;

    void Start()
    {
        RequestGecis();
        RequestGecisVideo();
    }


    void RequestGecis()
    {
#if UNITY_ANDROID
        Reklamid = AndroidBannerReklamKimligi;
#elif UNITY_IPHONE
                        Reklamid=�osBannerReklamKimligi;
#else
                        Reklamid = "Tan�ms�z Platform";
#endif

        InterGecisReklami = new InterstitialAd(Reklamid);

        AdRequest request = new AdRequest.Builder().Build();
        InterGecisReklami.LoadAd(request);


    }

    void RequestGecisVideo()
    {
#if UNITY_ANDROID
        Reklamid2 = VideoAndroidBannerReklamKimligi;
#elif UNITY_IPHONE
              Reklamid2=Video�osBannerReklamKimligi;
#else
              Reklamid2 = "Tan�ms�z Platform";
#endif

        InterGecisVideoReklami = new InterstitialAd(Reklamid2);


        AdRequest request = new AdRequest.Builder().Build();
        InterGecisVideoReklami.LoadAd(request);


    }


    public void GameOver()
    {

        if (InterGecisReklami.IsLoaded())
        {
            InterGecisReklami.Show();

        }
        else
        {
            RequestGecis();
        }
    }
    public void GameOverVideo()
    {

        if (InterGecisVideoReklami.IsLoaded())
        {
            InterGecisVideoReklami.Show();

        }
        else
        {
            RequestGecisVideo();
        }
    }

    void GecisReklamiKaldir()
    {

        InterGecisVideoReklami.Destroy();
    }
}
