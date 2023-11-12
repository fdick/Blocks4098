using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Views
{
    public class UIViewData : MonoBehaviour
    {
        [field: SerializeField] public GameObject TopGO { get; private set; }
        
        [field: SerializeField] public GameObject StartPanelGO { get; private set; }
        [field: SerializeField] public GameObject EndPanelGO { get; private set; }
    }
}