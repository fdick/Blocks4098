using System;
using UnityEngine;

namespace Code.Services
{
    public interface IInputService
    {
        public Vector2 PointerPosition { get; }
        public Action StartTap { get; set; }
        public Action<Vector2> HoldTap { get; set; }
        public Action EndTap { get; set; }
    }
}