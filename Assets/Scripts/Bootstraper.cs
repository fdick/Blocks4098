using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Views
{
    public class Bootstraper : MonoBehaviour, IAppodealInitializationListener
    {
        private const string APP_KEY = "";
        private const int AD_TYPES = Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO | Appodeal.MREC;

        private void Awake()
        {
            //init ironSource
            Appodeal.initialize(APP_KEY, AD_TYPES, this);
        }

        public void onInitializationFinished(List<string> errors)
        {
            //load setting and other data
            SceneManager.LoadScene(1);
        }
    }
}