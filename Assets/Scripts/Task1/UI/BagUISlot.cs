using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field:SerializeField] public Button CellBtn {get; private set;}
    [field:SerializeField] public Image ItemImage {get; private set;}
    
    [field:SerializeField] public TextMeshProUGUI BottomAngleNumber {get; private set;}
    public Action OnHighlighted { get; set; }
    public Action OnDeHighlighted { get; set; }

    private void OnDestroy()
    {
        OnHighlighted = null;
        OnDeHighlighted = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHighlighted?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDeHighlighted?.Invoke();   
    }
}
