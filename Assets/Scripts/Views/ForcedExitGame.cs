using System;
using Code.Datas;
using Code.Services;
using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class ForcedExitGame : MonoBehaviour
    {
        [Inject] private GameplayStateMachine _sm;
        [Inject] private ProgressService _progress;
        [Inject] private SceneData _sceneData;

        private void OnApplicationQuit()
        {
            _sm.Enter<EndState>();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(hasFocus)
                _sceneData.CleanSpawnRegion.CleanRegion();
            _progress.SaveProgress();
        }
    }
}