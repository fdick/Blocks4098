using UnityEngine;

namespace Code.Services
{
    public interface ICubeMovementService
    {
        public Vector3 GetMoveVelocity();
        public Vector3 GetPreparePosition(Vector3 currentPos, Vector2 delta);
    }
}