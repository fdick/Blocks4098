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
                SceneManager.LoadScene(1);
                return;
            }

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
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