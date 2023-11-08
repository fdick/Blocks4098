using System;
using System.Collections.Generic;
using Code.Factories;
using Code.Pools;
using UnityEngine;
using UnityEngine.Pool;

namespace Code.Services
{
    public class EffectsPoolService : IObjectsPool<ParticleSystem>, IDisposable
    {
        private IObjectPool<ParticleSystem> _pool;
        private int _poolCapacity;

        private EffectsFactory _effectsFactory;
        private List<ParticleSystem> _activeEffects;

        public EffectsPoolService(EffectsFactory effectsFactory, int poolCapacity)
        {
            _effectsFactory = effectsFactory;
            _poolCapacity = poolCapacity;
            _activeEffects = new List<ParticleSystem>();
            _pool = new ObjectPool<ParticleSystem>(CreateEffect, OnTakeFromPool, OnReturnToPool);
        }


        public ParticleSystem Get(int index)
        {
            var c = _pool.Get();
            _activeEffects.Add(c);
            return c;
        }

        public void Return(ParticleSystem cube)
        {
            var poolables = cube.GetComponents<IPoolable>();
            foreach (var p in poolables)
            {
                p.OnReturnToPool();
            }

            _activeEffects.Remove(cube);
            _pool.Release(cube);
        }

        public void Dispose()
        {
            _activeEffects.Clear();
            _pool.Clear();
        }

        public void InitializePoolByOriginCapacity()
        {
            var list = new List<ParticleSystem>();
            for (int i = 0; i < _poolCapacity; i++)
            {
                list.Add(_pool.Get());
            }

            foreach (var c in list)
            {
                _pool.Release(c);
            }
        }

        private ParticleSystem CreateEffect()
        {
            return _effectsFactory.CreateEffect("NewCubeEffect");
        }

        private void OnTakeFromPool(ParticleSystem effect)
        {
            effect.gameObject.SetActive(true);
        }

        private void OnReturnToPool(ParticleSystem effect)
        {
            effect.gameObject.SetActive(false);
        }
    }
}