using System;
using System.Collections.Generic;
using Code.Datas;
using Code.Factories;
using Code.Pools;
using Code.StateMachine.States;

namespace Code.StateMachine
{
    public class GameplayStateMachine : StateMachine
    {
        public GameplayStateMachine(IBasketFactory basketFactory, SceneData sceneData, CubesPoolService cubesPool)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(InitState)] = new InitState(this, basketFactory, sceneData, cubesPool),
                [typeof(StartState)] = new StartState(sceneData),
                [typeof(EndState)] = new EndState(sceneData)
            };
        }
    }
}