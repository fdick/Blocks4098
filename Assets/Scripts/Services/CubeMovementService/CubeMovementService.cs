using System.Collections;
using System.Collections.Generic;
using Code.Datas;
using UnityEngine;

namespace Code.Services
{
    public class CubeMovementService : ICubeMovementService
    {
        public CubeMovementService(SceneData sceneData)
        {
            _sceneData = sceneData;
        }

        private SceneData _sceneData;
        public readonly Vector3 forwardDirection = new Vector3(0, 0, 1);
        public readonly Vector3 jumpDirection = new Vector3(0, 1, 0.3f);

        public Vector3 GetMoveVelocity()
        {
            return forwardDirection * _sceneData.ActorConfiguration.MoveVelocity;
        }


        public Vector3 GetPreparePosition(Vector3 currentPos, Vector2 delta)
        {
            var range = _sceneData.CubeMovementConfiguration.MovementRange;
            var x = currentPos.x + delta.x / _sceneData.CubeMovementConfiguration.PointerSensivityResistance;

            x = Mathf.Clamp(
                x,
                _sceneData.CubeSpawnPoint.position.x - range,
                _sceneData.CubeSpawnPoint.position.x + range);
            return new Vector3(x, _sceneData.CubeSpawnPoint.position.y, _sceneData.CubeSpawnPoint.position.z);
        }

        public float GetJumpForce() => _sceneData.ActorConfiguration.JumpForce;
    }
}