using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

namespace Code.Views
{
    public class Bootstraper : MonoBehaviour, IUnityAdsInitializationListener
    {
        string _androidGameId = "5475178";
        [SerializeField] bool _testMode = true;
        private string _gameId;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("adsTerms", 0) == 0)
            {
                //show privice policy
                SimpleGDPR.ShowDialog(new TermsOfServiceDialog()
                        .SetTermsOfServiceLink(
                            "https://doc-hosting.flycricket.io/blocks4098-terms-of-use/bcc28111-695e-4c45-9e6c-b67bdc87e4f8/terms")
                        .SetPrivacyPolicyLink(
                            "https://doc-hosting.flycricket.io/blocks4098-privacy-policy/c104fe74-a0b0-489c-bc3c-4b3b887d145f/privacy"),
                    OnDiagClosed);
            }
            else
            {
                Application.targetFrameRate = 60;
                InitializeAds();
            }
        }

        private void OnDiagClosed()
        {
            PlayerPrefs.SetInt("adsTerms", 1);
            Application.targetFrameRate = 60;
            InitializeAds();
        }

        public void InitializeAds()
        {
#if UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("No internet!");
                SceneManager.LoadScene(1);
                return;
            }

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                print("Internet is enabled!");

                Advertisement.Initialize(_gameId, _testMode, this);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            //load setting and other data
            SceneManager.LoadScene(1);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
            SceneManager.LoadScene(1);
        }
    }
}