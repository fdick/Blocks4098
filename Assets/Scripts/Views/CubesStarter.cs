using System;
using Code.Actors;
using Code.Datas;
using Code.Pools;
using Code.Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Views
{
    public class CubesStarter : MonoBehaviour
    {
        [field: SerializeField] public bool Activated { get; set; }
        [Inject] private CubesPoolService _poolService;
        [Inject] private IInputService _inputService;
        [Inject] private SceneData _sceneData;
        [Inject] private ICubeMovementService _cubeMovementService;
        [Inject] private AudioService _audioService;
        private CubeMove _currentCube;
        private float _savedTime;
        [SerializeField] private AudioClip _startCubeSound;
        private CleanSpawnRegion _cleanSpawn;


        private void Awake()
        {
            _inputService.StartTap += OnStartTap;
            _inputService.HoldTap += SetCubePosition;
            _inputService.EndTap += StartCube;
        }

        private void Start()
        {
            _cleanSpawn = FindObjectOfType<CleanSpawnRegion>();
        }


        private void OnDestroy()
        {
            _inputService.StartTap -= OnStartTap;
            _inputService.HoldTap -= SetCubePosition;
            _inputService.EndTap -= StartCube;
        }


        private void OnStartTap()
        {
            if (!Activated)
                return;

            if (!CheckCD(_sceneData.CubeMovementConfiguration.SpawnDelay))
                return;

            if (_cleanSpawn.IsCubesOnSpawnPosition)
                return;
            if (_currentCube != null)
            {
                _poolService.Return(_currentCube.GetComponent<CubeView>());
            }

            _savedTime = Time.time;


            var c = _poolService.Get(GetRandomIndex());
            c.transform.SetPositionAndRotation(_sceneData.CubeSpawnPoint.position, Quaternion.identity);

            if (c.TryGetComponent(out CubeMove move))
            {
                _currentCube = move;
            }

            if (c.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.Sleep();
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }
        }

        private void SetCubePosition(Vector2 delta)
        {
            if (!Activated)
                return;
            if (_currentCube == null)
                return;


            _currentCube.transform.position =
                _cubeMovementService.GetPreparePosition(_currentCube.transform.position, delta);
            _currentCube.transform.rotation = Quaternion.identity;
        }

        private void StartCube()
        {
            if (!Activated)
                return;
            if (_currentCube == null)
                return;
            if (_currentCube.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
            }

            _currentCube.UpdateVelocity();
            _currentCube = null;

            _audioService.PlaySound(_startCubeSound);
        }

        private bool CheckCD(float cd)
        {
            return _savedTime == 0 || Time.time > _savedTime + cd;
        }

        private int GetRandomIndex()
        {
            return Random.Range(0, 4);
        }
    }
}