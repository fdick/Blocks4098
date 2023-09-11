using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BagRowCellView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject container;
    public Image cellImage;
    public Button cellBtn;

    public TextMeshProUGUI bottomAngleNumber;


    public Action OnHighlighted { get; set; }
    private Action OnDeHighlighted { get; set; }


    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(InvItem<ItemBase> data, Action onClickBtn = null,
        Action onHighlighted = null, Action onDeHighlight = null)
    {
        // this cell was outside the range of the data, so we disable the container.
        // Note: We could have disable the cell gameobject instead of a child container,
        // but that can cause problems if you are trying to get components (disabled objects are ignored).
        // container.SetActive(data != null);
        if (onHighlighted != null)
            OnHighlighted = onHighlighted;
        if (onDeHighlight != null)
            OnDeHighlighted = onDeHighlight;
        
        if (data != null)
        {
            // set the text if the cell is inside the data range
            cellImage.gameObject.SetActive(true);
            cellImage.sprite = data.item.InventoryImage;
            
           if (data.item.IsCountable)
            {
                bottomAngleNumber.gameObject.SetActive(true);
                bottomAngleNumber.text = data.count.ToString();
            }
            else
               bottomAngleNumber.gameObject.SetActive(false);
          
            cellBtn.onClick.RemoveAllListeners();
            cellBtn.onClick.AddListener(() => onClickBtn?.Invoke());
        }
        else
        {
            cellImage.gameObject.SetActive(false);
            bottomAngleNumber.gameObject.SetActive(false);
        }
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