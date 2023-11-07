using System;
using System.Collections;
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
        private Coroutine _endGameAfterTimeInsideFinishTriggerCoro;
        private float _endGameAfterTime = 2;

        private void OnTriggerEnter(Collider c)
        {
            if (c.CompareTag("Finish"))
            {
                if (_endGameAfterTimeInsideFinishTriggerCoro == null)
                    _endGameAfterTimeInsideFinishTriggerCoro = StartCoroutine(EndGameAfterTime());

                if (!_firstTime)
                {
                    _firstTime = true;
                    return;
                }

                _stateMachine.Enter<EndState>();
            }
        }

        private void OnTriggerExit(Collider c)
        {
            if (_endGameAfterTimeInsideFinishTriggerCoro != null)
            {
                StopCoroutine(_endGameAfterTimeInsideFinishTriggerCoro);
                _endGameAfterTimeInsideFinishTriggerCoro = null;
            }
        }

        private IEnumerator EndGameAfterTime()
        {
            yield return new WaitForSeconds(_endGameAfterTime);
            _stateMachine.Enter<EndState>();
        }

        public void OnReturnToPool()
        {
            _firstTime = false;
        }
    }
}