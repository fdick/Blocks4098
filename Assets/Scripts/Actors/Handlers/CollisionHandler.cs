using System;
using Code.Actors;
using Code.Pools;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private CubeView _cubeView;
        [Inject] private CubesPoolService _poolService;
        [Inject] private EffectsPoolService _effectsPoolService;
        [Inject] private ProgressService _progress;
        private const string CUBE_TAG = "Cube";
        
        //if true - same cube nominal
        public Action<bool> OnCollisionWithCube { get; set; }

        private void OnCollisionEnter(Collision c)
        {
            if (!c.gameObject.CompareTag(CUBE_TAG))
                return;

            if (c.gameObject.TryGetComponent<CubeView>(out var anCube))
            {
                if (anCube.Actor.Configuration.Index == _cubeView.Actor.Configuration.Index)
                {
                    _poolService.Return(anCube);
                    _poolService.Return(_cubeView);
                    var newC = _poolService.Get(_cubeView.Actor.Configuration.Index + 1);
                    if(newC == null)
                        return;
                    newC.transform.position = transform.position;
                    var effect = _effectsPoolService.Get(0);
                    effect.transform.position = transform.position;
                    
                    newC.GetComponent<CubeMove>().AddJumpForce();
                    _progress.SetRecord(_progress.CurrentRecord + newC.Actor.Configuration.GetDenomitaion());
                }
                OnCollisionWithCube?.Invoke(anCube.Actor.Configuration.Index == _cubeView.Actor.Configuration.Index);
            }
        }

        public void OnDestroy()
        {
            OnCollisionWithCube = null;
        }
    }
}