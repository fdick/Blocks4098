using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeMove : MonoBehaviour
    {
        [Inject] private CubeMovementService _cubeMovementService;
        private Rigidbody _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void UpdateVelocity()
        {
            _rigidbody.velocity = _cubeMovementService.GetMoveVelocity();
        }

        public void AddJumpForce()
        {
            AddForce(_cubeMovementService.jumpDirection, _cubeMovementService.GetJumpForce());
        }

        public void AddForce(Vector3 direction, float force)
        {
            _rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}