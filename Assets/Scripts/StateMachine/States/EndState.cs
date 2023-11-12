using AppodealAds.Unity.Common;
using Code.Datas;
using Code.Services;
using Code.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.StateMachine.States
{
    public class EndState : IState, IRewardedVideoAdListener
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
            _adsService.ShowRewardAd();
        }
        
        private void RestartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void onRewardedVideoLoaded(bool precache)
        {
            
        }

        public void onRewardedVideoFailedToLoad()
        {
        }

        public void onRewardedVideoShowFailed()
        {
        }

        public void onRewardedVideoShown()
        {
        }

        public void onRewardedVideoFinished(double amount, string name)
        {
            var cleanerRegion = GameObject.FindObjectOfType<CleanSpawnRegion>();
            cleanerRegion.CleanRegion();
            
            _sceneData.CubesStarter.Activated = true;
            _sceneData.EndPanel.gameObject.SetActive(false);
        }

        public void onRewardedVideoClosed(bool finished)
        {
        }

        public void onRewardedVideoExpired()
        {
        }

        public void onRewardedVideoClicked()
        {
        }
    }
}