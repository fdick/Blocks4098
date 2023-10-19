using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/CubesPoolConfig", fileName = "config")]
    public class CubesPoolConfig : ScriptableObject
    {
        [field: SerializeField] public int PoolCapacity { get; private set; }
    }
}
