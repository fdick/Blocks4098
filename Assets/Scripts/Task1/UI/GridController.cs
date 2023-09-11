using System;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class GridController : MonoBehaviour, IEnhancedScrollerDelegate
{
    public SmallList<InvItem<ItemBase>> data;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. The cell view will
    /// hold references to each row sub cell
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    [SerializeField] private int _numberOfCellsPerRow = 3;
    [SerializeField] private float _height = 100;

    public Action<int> OnCellSelected { get; set; }
    public Action<int> OnCellHighlighted { get; set; }
    public Action<int> OnCellDeHighlighted { get; set; }


    private void OnDestroy()
    {
        OnCellSelected = null;
        OnCellHighlighted = null;
        OnCellDeHighlighted = null;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)data.Count / (float)_numberOfCellsPerRow);
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return _height;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        BagCellView cellView = scroller.GetCellView(cellViewPrefab) as BagCellView;

        // data index of the first sub cell
        var di = dataIndex * _numberOfCellsPerRow;

        cellView.name = "Cell " + (di).ToString() + " to " + ((di) + _numberOfCellsPerRow - 1).ToString();

        // pass in a reference to our data set with the offset for this cell
        cellView.SetData(ref data, di, OnCellSelected, OnCellHighlighted, OnCellDeHighlighted);

        // return the cell to the scroller
        return cellView;
    }

    public void InitController(InvItem<ItemBase>[] loadData)
    {
        scroller.Init();
        scroller.Delegate = this;
        LoadData(loadData);
    }

    public void ReloadGrid(InvItem<ItemBase>[] newData = null)
    {
        if (newData != null)
        {
            LoadData(newData);
            return;
        }

        scroller.ReloadData();
    }

    /// <summary>
    /// Populates the data with a lot of records
    /// </summary>
    private void LoadData(InvItem<ItemBase>[] loadData)
    {
        // set up some simple data
        data = new SmallList<InvItem<ItemBase>>();
        for (var i = 0; i < loadData.Length; i++)
        {
            data.Add(loadData[i]);
        }

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
    }
}