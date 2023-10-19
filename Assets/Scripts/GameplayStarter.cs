using Code.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class GameplayStarter : MonoBehaviour
    {
        [Inject] private StateMachine.GameplayStateMachine _stateMachine;


        private void Awake()
        {
            _stateMachine.Enter<InitState>();
        }
    }
}
