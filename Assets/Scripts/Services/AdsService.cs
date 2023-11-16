using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Code.Services
{
    public enum BannerPositions
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public class AdsService : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public readonly string ANDROID_REWARDED_ID = "Rewarded_Android";
        public readonly string ANDROID_INTERSTITIAL_ID = "Interstitial_Android";
        public readonly string ANDROID_BANNER_ID = "Banner_Android";

        public Action<string> OnLoadedAd { get; set; }
        public Action OnLoadedBanner { get; set; }
        public Action<string, UnityAdsShowCompletionState> OnCompletedAd { get; set; }

        public void LoadAdUnit(string adUnitID, Action<string> onLoaded = null)
        {
            OnLoadedAd = onLoaded;
            Advertisement.Load(adUnitID, this);
        }

        public void ShowAdUnit(string adUnitID)
        {
            Advertisement.Show(adUnitID, this);
        }

        // Implement a method to call when the Load Banner button is clicked:
        public void LoadBanner(string bannerID, Action onBannerLoaded = null, BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER)
        {
            OnLoadedBanner = onBannerLoaded;
            Advertisement.Banner.SetPosition(bannerPosition);
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            Advertisement.Banner.Load(bannerID, options);
        }

        // Implement a method to call when the Show Banner button is clicked:
        public void ShowBanner(string bannerID)
        {
            // Set up options to notify the SDK of show events:
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };

            // Show the loaded Banner Ad Unit:
            Advertisement.Banner.Show(bannerID, options);
        }

        public void HideBanner(bool destroyBanner = false)
        {
            Advertisement.Banner.Hide(destroyBanner);
        }

        private void OnBannerShown()
        {
        }

        private void OnBannerHidden()
        {
        }

        private void OnBannerClicked()
        {
        }

        private void OnBannerError(string message)
        {
        }

        private void OnBannerLoaded()
        {
            OnLoadedBanner?.Invoke();
            OnLoadedBanner = null;
        }


        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Ad Loaded: " + placementId);
            OnLoadedAd?.Invoke(placementId);
            OnLoadedAd = null;
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log($"Showing Ad Unit {placementId}: state is {showCompletionState}");
            OnCompletedAd?.Invoke(placementId, showCompletionState);
            OnCompletedAd = null;
        }
    }
}