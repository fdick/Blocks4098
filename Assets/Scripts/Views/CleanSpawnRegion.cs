using System;
using System.Collections.Generic;
using System.Linq;
using Code.Actors;
using Code.Pools;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    [RequireComponent(typeof(Collider))]
    public class CleanSpawnRegion : MonoBehaviour
    {
        [Inject] private CubesPoolService _poolService;
        private List<CubeView> _cubes = new List<CubeView>();


        private void OnTriggerEnter(Collider c)
        {
            _cubes.Add(c.GetComponent<CubeView>());
        }

        private void OnTriggerExit(Collider c)
        {
            var view = c.GetComponent<CubeView>();
            if (view != null && _cubes.Contains(view))
                _cubes.Remove(view);
        }

        public void CleanRegion()
        {
            foreach (var c in _cubes)
            {
                _poolService.Return(c);
            }

            _cubes.Clear();
        }
    }
}