using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ReklamScripti : MonoBehaviour
{
    
    private BannerView bannerWiew;
    public string AndroidBannerReklamKimligi;
    public string IosBannerReklamKimligi;
    string Reklamid;

   

    void Start()
    {
        RequestBanner();
        
    }


    void RequestBanner()
    {
#if UNITY_ANDROID
        Reklamid = AndroidBannerReklamKimligi;
#elif UNITY_IPHONE
                Reklamid=IosBannerReklamKimligi;
#else
                Reklamid = "Tanýmsýz Platform";
#endif


        bannerWiew = new BannerView(Reklamid, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();

        bannerWiew.LoadAd(request);
    }

  

    void Update()
    {
        
    }
}
