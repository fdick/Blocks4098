using System;
using System.Collections;
using System.Collections.Generic;
using Code.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Views
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordText;
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


        private void OnChangeRecord(int val)
        {
            SetRecord(val);
        }
    }
}