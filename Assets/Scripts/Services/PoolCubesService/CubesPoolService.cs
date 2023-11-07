using System;
using System.Collections.Generic;
using Code.Actors;
using Code.Factories;
using Code.Services;
using UnityEngine.Pool;

namespace Code.Pools
{
    public class CubesPoolService : IObjectsPool<CubeView>, IDisposable
    {
        private IObjectPool<CubeView> _pool;
        private int _poolCapacity;

        private ICubeFactory _cubeFactory;
        private ICubeConfigurationService _cubeConfiguration; 
        
        private List<CubeView> _activeCubes;

        public CubesPoolService(ICubeFactory cubeFactory, ICubeConfigurationService cubeConfiguration, int poolCapacity)
        {
            _cubeFactory = cubeFactory;
            _cubeConfiguration = cubeConfiguration;
            _poolCapacity = poolCapacity;
            _activeCubes = new List<CubeView>();
            _pool = new ObjectPool<CubeView>(CreateCube, OnTakeCubeFromBall, OnReturnCubeToPool);
        }


        public CubeView Get(int index)
        {
            var c = _pool.Get();
            _cubeConfiguration.Configure(c, index);
            _activeCubes.Add(c);
            return c;
        }

        public void Return(CubeView cube)
        {
            var poolables = cube.GetComponents<IPoolable>();
            foreach (var p in poolables)
            {
                p.OnReturnToPool();
            }
            _activeCubes.Remove(cube);
            _pool.Release(cube);
        }
        
        public void Dispose()
        {
            _activeCubes.Clear();
            _pool.Clear();
        }
        
        public void InitializePoolByOriginCapacity()
        {
            var list = new List<CubeView>();
            for (int i = 0; i < _poolCapacity; i++)
            {
                list.Add(_pool.Get());
            }

            foreach (var c in list)
            {
                _pool.Release(c);
            }
        }

        private CubeView CreateCube()
        {
            return _cubeFactory.CreateCube(2);
        }

        private void OnTakeCubeFromBall(CubeView cube)
        {
            cube.gameObject.SetActive(true);
        }

        private void OnReturnCubeToPool(CubeView cube)
        {
            cube.gameObject.SetActive(false);
        }
    }
}