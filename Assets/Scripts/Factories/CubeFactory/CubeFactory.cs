using System.Collections;
using System.Collections.Generic;
using Code.Actors;
using Code.Factories;
using Code.Services;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Factories
{
    public class CubeFactory : ICubeFactory
    {
        private AssetProvider _assetProvider;
        private DiContainer _DIContainer;

        public CubeFactory(AssetProvider assetProvider, DiContainer DIContainer)
        {
            _assetProvider = assetProvider;
            _DIContainer = DIContainer;
        }

        private const string CUBE_PREFAB_PATH = "CubeActor";

        [CanBeNull]
        public CubeView CreateCube(int cubeID)
        {
            var cubeRef = _assetProvider.Load<GameObject>(CUBE_PREFAB_PATH);
            return _DIContainer.InstantiatePrefab(cubeRef).GetComponent<CubeView>();
        }
    }
}