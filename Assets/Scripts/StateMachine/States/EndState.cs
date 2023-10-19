using System.Collections;
using System.Collections.Generic;
using Code.Datas;
using Code.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.StateMachine.States
{
    public class EndState : IState
    {
        public EndState(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        private SceneData _sceneData;
        
        public void Enter()
        {
            _sceneData.CubesStarter.Activated = false;
            _sceneData.EndPanel.gameObject.SetActive(true);
            _sceneData.EndPanel.RestartBtn.onClick.AddListener(RestartGame);
        }

        public void Exit()
        {
            
        }
        
        
        private void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

    }
}