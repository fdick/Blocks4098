using Code.Services;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Factories
{
    public class BasketFactory : IBasketFactory
    {
        private IAssetProvider _assetProvider;
        private DiContainer _container;
        private const string MAP_PREFAB_PATH = "Basket";

        public BasketFactory(IAssetProvider assetProvider, DiContainer container)
        {
            _assetProvider = assetProvider;
            _container = container;
        }

        [CanBeNull]
        public BasketView CreateMap()
        {
            var prefab = _assetProvider.Load<GameObject>(MAP_PREFAB_PATH);
            
            return _container.InstantiatePrefab(prefab).GetComponent<BasketView>();
        }
    }
}
