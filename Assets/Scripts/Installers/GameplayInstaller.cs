using Code.Datas;
using Code.Factories;
using Code.Pools;
using Code.Services;
using Code.StateMachine;
using Code.Views;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private SceneData _sceneData;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BasketFactory>().FromNew().AsSingle();
            var assetProvider = new AssetProvider();
            var cubeFactory = new CubeFactory(assetProvider, Container);
            var cubeConfiguration = new CubeConfigurationService(_sceneData);
            var cubesPool = new CubesPoolService(cubeFactory, cubeConfiguration, _sceneData.CubesPoolConfig.PoolCapacity);
            var cubeMove = new CubeMovementService(_sceneData);
            var effectFactory = new EffectsFactory(assetProvider, Container);
            var effectPool = new EffectsPoolService(effectFactory, _sceneData.CubesPoolConfig.PoolCapacity);
            var audio = new AudioService(_sceneData.BackgroundMusicSource, _sceneData.GameplaySoundsSource);
            var saveLoad = new SaveLoadService();
            var ads = new AdsService();
            var progress = new ProgressService(saveLoad, ads);
            Container.BindInterfacesAndSelfTo<CubeFactory>().FromInstance(cubeFactory).AsSingle();
            Container.Bind<CubesPoolService>().FromInstance(cubesPool).AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().FromNew().AsSingle();
            Container.Bind<SaveLoadService>().FromInstance(saveLoad).AsSingle();
            Container.Bind<ProgressService>().FromInstance(progress).AsSingle();
            Container.BindInterfacesAndSelfTo<CubeMovementService>().FromInstance(cubeMove).AsSingle();
            Container.Bind<SceneData>().FromInstance(_sceneData).AsSingle();
            Container.Bind<GameplayStateMachine>().FromNew().AsSingle();
            Container.Bind<EffectsPoolService>().FromInstance(effectPool).AsSingle();
            Container.Bind<AudioService>().FromInstance(audio).AsSingle();
            Container.Bind<AdsService>().FromInstance(ads).AsSingle();
        }
    }
}