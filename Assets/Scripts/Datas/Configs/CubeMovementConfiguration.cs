using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Datas
{
    [CreateAssetMenu(menuName = "Configs/CubeMovementConfig", fileName = "CubeMovementConfig")]
    public class CubeMovementConfiguration : ScriptableObject
    {
        [field: SerializeField] public float PointerSensivityResistance { get; private set; } = 200;
        [field: SerializeField] public float MovementRange { get; private set; } = 1.5f;
        [field: SerializeField] public float SpawnDelay { get; private set; } = 1f;
    }
}