using Code.Services;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Factories
{
    public class BasketFactory : IBasketFactory
    {
        private IAssetProvider _assetProvider;
        private const string MAP_PREFAB_PATH = "Basket";

        public BasketFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        [CanBeNull]
        public BasketView CreateMap()
        {
            var prefab = _assetProvider.Load<GameObject>(MAP_PREFAB_PATH);
            return GameObject.Instantiate(prefab).GetComponent<BasketView>();
        }
    }
}
