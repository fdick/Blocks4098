using Code.Services;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Factories
{
    public class EffectsFactory
    {
        private AssetProvider _assetProvider;
        private DiContainer _DIContainer;

        public EffectsFactory(AssetProvider assetProvider, DiContainer DIContainer)
        {
            _assetProvider = assetProvider;
            _DIContainer = DIContainer;
        }


        [CanBeNull]
        public ParticleSystem CreateEffect(string effectName)
        {
            var cubeRef = _assetProvider.Load<GameObject>(effectName);
            if (cubeRef == null)
                return null;
            return _DIContainer.InstantiatePrefab(cubeRef).GetComponent<ParticleSystem>();
        }
    }
}