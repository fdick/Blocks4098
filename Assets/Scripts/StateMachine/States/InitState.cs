using Code.Configs;
using Code.Datas;
using Code.Factories;
using Code.Pools;
using Code.Services;
using Code.Views;
using UnityEngine;

namespace Code.StateMachine.States
{
    public class InitState : IState
    {
        public InitState(GameplayStateMachine stateMachine, IBasketFactory basketFactory, SceneData sceneData,
            CubesPoolService cubesPoolService, EffectsPoolService effectPoolService)
        {
            _stateMachine = stateMachine;
            _basketFactory = basketFactory;
            _sceneData = sceneData;
            _cubesPoolService = cubesPoolService;
            _effectsPoolService = effectPoolService;
        }

        private IBasketFactory _basketFactory;
        private SceneData _sceneData;
        private GameplayStateMachine _stateMachine;
        private CubesPoolService _cubesPoolService;
        private EffectsPoolService _effectsPoolService;


        public void Enter()
        {
            //create map
            var map = _basketFactory.CreateMap();
            map.transform.position = _sceneData.BasketSpawnPoint.position;
            //set cube spawn point
            _sceneData.CubeSpawnPoint = map.CubeSpawnPoint;
            //enable ui
            GameObject.FindObjectOfType<TopPanelView>(true).gameObject.SetActive(true);
            //init pool
            _cubesPoolService.InitializePoolByOriginCapacity();
            _effectsPoolService.InitializePoolByOriginCapacity();

            _stateMachine.Enter<StartState>();
        }

        public void Exit()
        {
        }
    }
}