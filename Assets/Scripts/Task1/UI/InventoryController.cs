using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryUIRefs _data;
    [SerializeField] private GridController _gridController;
    [SerializeField] private int _inventoryCapacity = 20;
    [SerializeField] private bool _loadStartableItems;
    [SerializeField] private ItemBase[] _startableItems;
    [SerializeField] private bool _initOnAwake;
    [SerializeField] private bool _useNewPrices;

    private Inventory<ItemBase> _inventory;

    public Inventory<ItemBase> Inventory { get => _inventory; }
    public Action<int> OnCellClick { get; set; }

#if UNITY_EDITOR
    [SerializeField] private DebugInventoryController _debug = new DebugInventoryController();
#endif

    private void Awake()
    {
        if (_initOnAwake)
            InitInventory();
    }

#if UNITY_EDITOR
    private void Update()
    {
        _debug.Fill(_inventory.Vault.ToList());
    }
#endif

    private void OnDestroy()
    {
        OnCellClick = null;
    }

    public void InitInventory()
    {
        _inventory = new Inventory<ItemBase>(_inventoryCapacity);
        SetStartableItems();

        _gridController.OnCellHighlighted += OnCellHighlight;
        _gridController.OnCellDeHighlighted += OnCellDeHighlight;
        _gridController.OnCellSelected += OnCellClicking;

        _gridController.InitController(_inventory.Vault);
        print("Grid was inited");
    }

    public int TryAddItem(InvItem<ItemBase> item)
    {
        return TryAddItem(item.item, item.count);
    }

    public int TryAddItem(ItemBase item, int count = 1)
    {
        var index = _inventory.AddItem(item, count);
        if (index < 0)
            return -1;

        _gridController.ReloadGrid(_inventory.Vault);
        return index;
    }

    public int TryRemoveItem(InvItem<ItemBase> item, int count = 0)
    {
        var index = _inventory.RemoveItem(item, count);
        if (index < 0)
            return -1;

        _gridController.ReloadGrid(_inventory.Vault);
        return index;
    }

    private void SetStartableItems()
    {
        if (_loadStartableItems)
        {
            for (int i = 0; i < _startableItems.Length; i++)
            {
                _inventory.AddItem(_startableItems[i]);
            }
        }
    }

    private void OnCellHighlight(int index)
    {
        var invItem = _inventory[index];
        if(invItem == null)
            return;
        var p = PriceManager.Instance.ShopPriceList.GetPrice(invItem.item.ItemName);
        if (p == null)
            return;
        
        int price = _useNewPrices ? p.NewProductPrice : p.UsedProductPrice;

        price *= invItem.count;

        SetPriceText(price.ToString());
    }

    private void OnCellDeHighlight(int index)
    {
        SetPriceText(null);
    }

    private void OnCellClicking(int index)
    {
        OnCellClick?.Invoke(index);
    }

    private void SetPriceText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            _data.BotGO.SetActive(false);
            return;
        }

        _data.BotGO.SetActive(true);
        _data.PriceValueText.text = text;
    }
}

[Serializable]
public class DebugInventoryController
{
    [Serializable]
    public class ItemSlot
    {
        public string name;
        public int count;
    }

    public List<ItemSlot> items = new List<ItemSlot>();

    public void Fill(List<InvItem<ItemBase>> items)
    {
        this.items.Clear();
        foreach (var it in items)
        {
            if (it == null || it.item == null)
            {
                this.items.Add(new ItemSlot() { name = "none", count = 0 });
                continue;
            }

            try
            {
                this.items.Add(new ItemSlot()
                    { name = it.item.ItemName, count = it.count });
            }
            catch (Exception e)
            {
                this.items.Add(new ItemSlot() { name = "none", count = 0 });
            }
        }
    }
}