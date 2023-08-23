using System;
using UnityEngine;

public class TriggerProcess : MonoBehaviour
{
    public Action<Collider2D> OnTriggerEnter { get; set; }
    public Action<Collider2D> OnTriggerExit { get; set; }
    private void OnTriggerEnter2D(Collider2D c)
    {
        OnTriggerEnter?.Invoke(c);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        OnTriggerExit?.Invoke(c);
    }

    private void OnDestroy()
    {
        OnTriggerEnter = null;
        OnTriggerExit = null;
    }
}
