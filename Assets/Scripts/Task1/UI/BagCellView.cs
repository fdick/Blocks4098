using System;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class BagCellView : EnhancedScrollerCellView
{
    public GameObject container;
    [SerializeField] private BagRowCellView[] _rows;

    /// <summary>
    /// This function just takes the Demo data and displays it
    /// </summary>
    /// <param name="data"></param>
    public void SetData(ref SmallList<InvItem<ItemBase>> data, int startingIndex,
        Action<int> onSelected, Action<int> onHighlighted, Action<int> onDeHighlighted)
    {
        // this cell was outside the range of the data, so we disable the container.
        // Note: We could have disable the cell gameobject instead of a child container,
        // but that can cause problems if you are trying to get components (disabled objects are ignored).
        container.SetActive(data != null);

        if (data != null)
        {
            // set the text if the cell is inside the data range
            for (int i = 0; i < _rows.Length; i++)
            {
                var index = startingIndex + i;
                var invItem = data[index];
                
                

                // print($"Bind is {bind}");
                
                _rows[i].SetData(
                    data[index],
                    () => onSelected?.Invoke(index),
                    () => onHighlighted?.Invoke(index),
                    () => onDeHighlighted?.Invoke(index));
            }
        }
    }
}