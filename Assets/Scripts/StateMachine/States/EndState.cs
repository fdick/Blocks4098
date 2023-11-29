using Code.Datas;
using Code.Services;
using Code.Views;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

namespace Code.StateMachine.States
{
    public class EndState : IState
    {
        public EndState(SceneData sceneData, ProgressService progress, AdsService adService)
        {
            _sceneData = sceneData;
            _progressService = progress;
            _adsService = adService;
        }

        private SceneData _sceneData;
        private ProgressService _progressService;
        private AdsService _adsService;

        public void Enter()
        {
            _sceneData.CubesStarter.Activated = false;
            _sceneData.EndPanel.gameObject.SetActive(true);
            _sceneData.EndPanel.RestartBtn.onClick.AddListener(RestartGame);
            _sceneData.EndPanel.ContinueBtn.onClick.AddListener(ContinueGame);

            //save data
            _progressService.SaveProgress();
        }

        public void Exit()
        {
        }

        private void ContinueGame()
        {
            _adsService.LoadAdUnit(_adsService.ANDROID_REWARDED_ID, 
                adUnitId =>
            {
                if (!adUnitId.Equals(_adsService.ANDROID_REWARDED_ID))
                    return;

                _adsService.ShowAdUnit(_adsService.ANDROID_REWARDED_ID);
                _adsService.OnCompletedAd += (adUnit, showCompletionState) =>
                {

                    if (adUnit.Equals(_adsService.ANDROID_REWARDED_ID) &&
                        showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
                    {
                        Debug.Log("Unity Ads Rewarded Ad Completed");
                        // Grant a reward.
                        onRewardedVideoFinished();
                    }
                    _sceneData.CleanSpawnRegion.CleanRegion();

                    _sceneData.EndPanel.ContinueBtn.interactable = true;
                };
            });
            _sceneData.EndPanel.ContinueBtn.interactable = false;

        }

        private void RestartGame()
        {
            SceneManager.LoadScene(1);
        }


        public void onRewardedVideoFinished()
        {
            var cleanerRegion = GameObject.FindObjectOfType<CleanSpawnRegion>();
            cleanerRegion.CleanRegion();

            _sceneData.CubesStarter.Activated = true;
            _sceneData.EndPanel.gameObject.SetActive(false);
        }


        // public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        // {
        // }
        //
        // public void OnUnityAdsShowStart(string placementId)
        // {
        // }
        //
        // public void OnUnityAdsShowClick(string placementId)
        // {
        // }
        //
        // // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        // public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        // {
        //     if (adUnitId.Equals(_adsService.ANDROID_REWARDED_ID) &&
        //         showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        //     {
        //         Debug.Log("Unity Ads Rewarded Ad Completed");
        //         // Grant a reward.
        //         onRewardedVideoFinished();
        //     }
        //     
        // }
    }
}