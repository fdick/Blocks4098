using Code.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordText;
        [SerializeField] private TextMeshProUGUI _lastRecordText;
        [Inject] private ProgressService _progress;

        private void Awake()
        {
            _progress.OnChangeRecord += OnChangeRecord;
        }

        private void OnDestroy()
        {
            _progress.OnChangeRecord -= OnChangeRecord;
        }


        public void SetRecord(int record)
        {
            _recordText.text = record.ToString();
        }
        
        public void SetLastRecord(int record)
        {
            _lastRecordText.text = record.ToString();
        }


        private void OnChangeRecord(int val)
        {
            SetRecord(val);
        }
    }
}