using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/ActorConfig", fileName = "config")]
    public class ActorConfiguration : ScriptableObject
    {
        [field: SerializeField] public float MoveVelocity { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
    }
}