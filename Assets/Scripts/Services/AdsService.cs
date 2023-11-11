using System;
using AppodealAds.Unity.Api;
using UnityEngine;

namespace Code.Services
{
    public enum BannerPositions
    {
        Top,
        Right,
        Bottom,
        Left
    }
    
    public class AdsService
    {
        //Banner ad
        public void LoadBanner()
        {
            
        }

        public void ShowHideBanner(bool show, BannerPositions bannerPos)
        {
            if (!show)
            {
                Appodeal.hide(Appodeal.BANNER);
                return;
            }
            
            switch (bannerPos)
            {
                case BannerPositions.Top:
                    Appodeal.show(Appodeal.BANNER_TOP);
                    break;
                case BannerPositions.Right:
                    Appodeal.show(Appodeal.BANNER_RIGHT);
                    break;
                case BannerPositions.Bottom:
                    Appodeal.show(Appodeal.BANNER_BOTTOM);
                    break;
                case BannerPositions.Left:
                    Appodeal.show(Appodeal.BANNER_LEFT);
                    break;
            }
        }
        
        //reward ad
        public void LoadRewardAd()
        {
            
        }

        public void ShowRewardAd()
        {
            if (!Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            {
                Debug.Log($"{Appodeal.REWARDED_VIDEO} Ad is not loaded!");
                return;
            }
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        
        //Interstition ad
        public void LoadInterstitionAd()
        {
            
        }

        public void ShowInterstitialAd()
        {
            if (!Appodeal.isLoaded(Appodeal.INTERSTITIAL))
            {
                Debug.Log($"{Appodeal.INTERSTITIAL} Ad is not loaded!");
                return;
            }
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
    }
}