using Code.Datas;
using Code.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Views
{
    public class InputUpdator : MonoBehaviour, IPointerMoveHandler
    {
        [Inject] private InputService _inputService;
        [Inject] private SceneData _sceneData;

        private void Awake()
        {
            _inputService.StartTap += OnStartTap;
        }

        private void Update()
        {
            _inputService.UpdateInput();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _inputService.HoldTap?.Invoke(eventData.delta);
        }

        private void OnStartTap()
        {
            _sceneData.StartPanelGO.SetActive(false);
        }
    }
}