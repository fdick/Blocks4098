using System;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private InventoryController _bagsInventory;
    [SerializeField] private InventoryController _shopsInventoryCtrl;
    [SerializeField] private GoldWidgetController _goldWidget;

    private void Awake()
    {
        print("Awake");
        _bagsInventory.InitInventory();
        _shopsInventoryCtrl.InitInventory();

        _bagsInventory.OnCellClick += OnSellItemFromBag;
        _shopsInventoryCtrl.OnCellClick += OnBuyItemFromShop;
    }

    private void Start()
    {
        _bagsInventory.gameObject.SetActive(true);
        _shopsInventoryCtrl.gameObject.SetActive(true);
    }

    private void OnBuyItemFromShop(int index)
    {
        if (TryBuyItem(index))
        {
            print($"You bought an item");
        }
    }

    private void OnSellItemFromBag(int index)
    {
        var invItem = _bagsInventory.Inventory[index];
        if (invItem == null)
            return;
        if (TrySellItem(invItem))
        {
            print($"You have sold an item {invItem.item.ItemName} - Count {invItem.count}");
        }
    }

    public bool TrySellItem(InvItem<ItemBase> invItem)
    {
        var p = PriceManager.Instance.ShopPriceList.GetPrice(invItem.item.ItemName);
        if (p == null)
        {
            return false;
        }

        int price = p.UsedProductPrice;
        price *= invItem.count;


        //change gold
        _goldWidget.AddValue(price);
        //change inventory
        if (_shopsInventoryCtrl.TryAddItem(invItem) < 0)
            return false;
        _bagsInventory.TryRemoveItem(invItem);
        return true;
    }

    public bool TryBuyItem(int itemIndex)
    {
        var invItem = _shopsInventoryCtrl.Inventory[itemIndex];
        if (invItem == null)
            return false;
        var p = PriceManager.Instance.ShopPriceList.GetPrice(invItem.item.ItemName);
        if (p == null)
            return false;
        int price = p.NewProductPrice;
        price *= invItem.count;

        var wallet = _goldWidget.GoldCurrency;
        print($"You try to buy {invItem.item.ItemName}");
        if (price > wallet)
        {
            print($"Dont have gold! Price {price} - Wallet {wallet}");
            return false;
        }

        //change gold
        _goldWidget.AddValue(-price);

        //change inventory
        if (_bagsInventory.TryAddItem(invItem) < 0)
            return false;
        _shopsInventoryCtrl.TryRemoveItem(invItem);

        return true;
    }
}