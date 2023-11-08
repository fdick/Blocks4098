using System;
using System.Collections.Generic;
using Code.Datas;
using Code.Factories;
using Code.Pools;
using Code.Services;
using Code.StateMachine.States;

namespace Code.StateMachine
{
    public class GameplayStateMachine : StateMachine
    {
        public GameplayStateMachine(IBasketFactory basketFactory, SceneData sceneData, CubesPoolService cubesPool,
            AudioService audioService, EffectsPoolService effectPoolService)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(InitState)] = new InitState(this, basketFactory, sceneData, cubesPool, effectPoolService),
                [typeof(StartState)] = new StartState(sceneData, audioService),
                [typeof(EndState)] = new EndState(sceneData)
            };
        }
    }
}