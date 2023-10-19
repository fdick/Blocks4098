using Code.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Views
{
    public class InputUpdator : MonoBehaviour, IPointerMoveHandler
    {
        [Inject] private InputService _inputService;

        private void Update()
        {
            _inputService.UpdateInput();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _inputService.HoldTap?.Invoke(eventData.delta);
        }
    }
}