using UnityEngine;
using UnityEngine.Serialization;

public class MoveHandler : MonoBehaviour
{
    [field:SerializeField] public bool EnableMove { get; set; } = true;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _moveDirection;

    private void Update()
    {
        if(!EnableMove)
            return;
        
        var dir = new Vector2(_moveDirection.x * _moveSpeed * Time.deltaTime, _moveDirection.y);
        transform.Translate(dir);
    }
}
