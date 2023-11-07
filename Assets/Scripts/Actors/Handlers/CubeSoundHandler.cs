using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class CubeSoundHandler : MonoBehaviour
    {
        [SerializeField] private CollisionHandler _collision;
        [Inject] private AudioService _audioService;
        [SerializeField] private AudioClip _sameCubeCollisionSound;
        [SerializeField] private AudioClip _cubeCollisionSound;

        private void Awake()
        {
            _collision.OnCollisionWithCube += OnCollisionWithCube;
        }

        private void OnCollisionWithCube(bool sameCube)
        {
            if (sameCube)
            {
                _audioService.PlaySound(_sameCubeCollisionSound);
            }
            else
            {
                _audioService.PlaySound(_cubeCollisionSound);
            }
        }
    }
}