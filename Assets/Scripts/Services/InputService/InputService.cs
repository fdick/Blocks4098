using System;
using UnityEngine;

namespace Code.Services
{
    public class InputService : IInputService
    {
        public Vector2 PointerPosition { get; private set; }
        public Action StartTap { get; set; }
        public Action<Vector2> HoldTap { get; set; }
        public Action EndTap { get; set; }

        public void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartTap?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                EndTap?.Invoke();
            }

            PointerPosition = Input.mousePosition;
            
        }
    }
}