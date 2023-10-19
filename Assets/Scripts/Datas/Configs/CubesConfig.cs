using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/CubesConfig", fileName = "config")]
    public partial class CubesConfig : ScriptableObject
    {
        [field: SerializeField] public CubeConfiguration[] CubesConfigurations { get; private set; }
    }
}