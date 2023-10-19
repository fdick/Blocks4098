using Code.StateMachine;
using Code.StateMachine.States;
using UnityEngine;
using Zenject;
using IPoolable = Code.Services.IPoolable;

namespace Code.Views
{
    public class TriggerHandler : MonoBehaviour, IPoolable
    {
        [Inject] private GameplayStateMachine _stateMachine;
        private bool _firstTime; // at first time a cube will be spawn inside an end trigger zone

        private void OnTriggerEnter(Collider c)
        {
            if (c.CompareTag("Finish"))
            {
                if (!_firstTime)
                {
                    _firstTime = true;
                    return;
                }

                _stateMachine.Enter<EndState>();
            }
        }

        public void OnReturnToPool()
        {
            _firstTime = false;
        }
    }
}