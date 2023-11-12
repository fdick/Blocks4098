using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class ForcedExitGame : MonoBehaviour
    {
        [Inject] private GameplayStateMachine _sm;

        private void OnApplicationQuit()
        {
            _sm.Enter<EndState>();
        }
    }
}