using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

/// <summary>
/// Simple inventory with countable opportunity
/// </summary>
/// <typeparam name="T">Type of contained items</typeparam>
/// <typeparam name="T1">Type of unique item names. This is enum! This need only for comfortable use</typeparam>
[Serializable]
public class Inventory<T> : IEnumerable<T> where T : class, IInventoryItem
{
    private InvItem<T>[] _itemsVault;

    private int _capacity;
    private int _countBusySlots;

    public InvItem<T>[] Vault => _itemsVault;
    public int Capacity => _capacity;

    public Inventory(int capacity)
    {
        _capacity = capacity;
        _itemsVault = new InvItem<T>[_capacity];
    }


    public IEnumerator<T> GetEnumerator()
    {
        return Vault.GetEnumerator() as IEnumerator<T>;
    }

    ~Inventory()
    {
        DestroyInventory();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int AddItem(InvItem<T> invItem)
    {
        return AddItem(invItem.item, invItem.count);
    }

    public int AddItem(T item, int count = 1)
    {
        if (item == null)
        {
            Debug.Log("Input object is null");
            return -1;
        }


        if (item.IsCountable)
        {
            if (!TryAddCountableItem(item, count, out var index))
                return -1;
            // Debug.Log($"Add countable item {index}");
            return index;
        }

        if (!HaveIFreeSlots())
        {
            Debug.Log("Inventory is full");
            return -1;
        }

        int freeSlotId = GetFreeSlotId();
        if (freeSlotId == -1)
        {
            Debug.Log("Inventory is full!");
            return -1;
        }

        var invItem = new InvItem<T>(item, 1);
        _itemsVault[freeSlotId] = invItem;
        _countBusySlots++;
        return freeSlotId;
    }

    private bool RemoveItem(InvItem<T> item)
    {
        if (item == null || !Contains(item, out int outItemId))
        {
            Debug.LogError("Inventory cant delete this item");
            return false;
        }

        _itemsVault[outItemId] = null;
        _countBusySlots--;
        return true;
    }
    /// <summary>
    /// Remove item from inventory.
    /// </summary>
    /// <param name="item">Item of vault</param>
    /// <param name="removableCount">If == 0 than full remove item</param>
    /// <returns> If 0 - was removed the full item. If 1 - was removed one of count. If -1 - was not removed the item</returns>
    public int RemoveItem(InvItem<T> item, int removableCount = 0)
    {
        if (item == null || !Contains(item, out int outItemId))
        {
            Debug.LogError("Inventory cant delete this item");
            return -1;
        }

        if (removableCount == 0 || !item.item.IsCountable)
            return RemoveItem(item) ? 0 : -1;
        else
        {
            item.count -= removableCount;
            if (item.count == 0)
                return RemoveItem(item) ? 0 : -1;
        }

        return 1;
    }

    private bool RemoveAt(int index)
    {
        if (index >= Capacity)
        {
            Debug.LogError("Inventory cant delete this item");
            return false;
        }

        _itemsVault[index] = default;
        _countBusySlots--;

        return true;
    }

    /// <summary>
    /// Remove item from inventory by index.
    /// </summary>
    /// <param name="index">Index of vault.</param>
    /// <param name="removableCount">If == 0 than full remove item.</param>
    public bool RemoveAt(int index, int removableCount = 0)
    {
        if (index >= Capacity)
        {
            Debug.LogError("Inventory cant delete this item");
            return false;
        }

        var item = _itemsVault[index];
        if (removableCount == 0 || !item.item.IsCountable)
            return RemoveAt(index);
        else
        {
            item.count -= removableCount;
            if (item.count == 0)
                return RemoveAt(index);
        }

        return true;
    }


    public bool HaveIFreeSlots()
    {
        return _countBusySlots < _capacity;
    }

    public bool Contains(InvItem<T> item, out int outItemIndex)
    {
        outItemIndex = -1;
        if (item == null)
            return false;
        for (int i = 0; i < _itemsVault.Length; i++)
        {
            if (_itemsVault[i] == null)
                continue;

            if (item.UniqueID == _itemsVault[i].UniqueID)
            {
                outItemIndex = i;
                return true;
            }
        }

        //if not contain than return false
        outItemIndex = -1;
        return false;
    }

    public bool Contains(Type type, out int outItemIndex)
    {
        outItemIndex = -1;
        for (int i = 0; i < _itemsVault.Length; i++)
        {
            var item = _itemsVault[i];

            if (item == null)
                continue;

            if (item.item.GetType() == type)
            {
                outItemIndex = i;
                return true;
            }
        }

        return false;
    }

    public int GetFreeSlotId()
    {
        for (int i = 0; i < _itemsVault.Length; i++)
        {
            if (_itemsVault[i] == null)
                return i;
        }

        //Debug.LogError("Not enough slots in inventory!");
        return -1;
    }

    public InvItem<T> this[int index]
    {
        get => _itemsVault[index];
        set => _itemsVault[index] = value;
    }

    public int IndexOf(InvItem<T> countableItem)
    {
        if (Contains(countableItem, out var index))
            return index;
        Debug.LogError($"Invalid index - {index}. Vault count - {Capacity}");
        return -1;
    }

    public void Sort(Func<InvItem<T>, int?> comparer = null)
    {
        if (comparer != null)
            _itemsVault = _itemsVault.OrderByDescending(comparer).ToArray();
        else
            _itemsVault.OrderByDescending((x) => x.item.GetType());
    }

    public void ResizeInventory(int newCapacity)
    {
        if (newCapacity <= 0)
            return;

        var vault = new InvItem<T>[newCapacity];

        Sort(); // we need position all items in stroke
        Array.Copy(_itemsVault, vault, _countBusySlots > newCapacity ? _countBusySlots - newCapacity : _countBusySlots);

        //clear
        _itemsVault = null;
        _countBusySlots = 0;

        //init new vault
        _itemsVault = vault;
        _capacity = newCapacity;
    }

    public void Clear()
    {
        _itemsVault = new InvItem<T>[_capacity];
        _countBusySlots = 0;
    }

    private void DestroyInventory()
    {
        _itemsVault = null;
    }

    private bool TryAddCountableItem(T item, int count, out int itemIndex)
    {
        itemIndex = -1;
        if (!item.IsCountable)
            return false;
        var notFilledItem = GetNotFilledCountableItem(item.GetType(), out itemIndex);
        if (notFilledItem != null)
        {
            var endCount = notFilledItem.count + count;
            var rest = 0;
            if (endCount > notFilledItem.item.MaxCounts)
            {
                rest = endCount - notFilledItem.item.MaxCounts;
                endCount = notFilledItem.item.MaxCounts;
            }

            notFilledItem.count = endCount;

            //if have rest(�������)
            if (rest > 0)
                return TryAddCountableItem(item, rest, out itemIndex);
            else
                return true;
        }
        else
        {
            var endCount = count > item.MaxCounts ? item.MaxCounts : count;
            var rest = 0;
            if (count > item.MaxCounts)
                rest = count - item.MaxCounts;

            var newInvItem = new InvItem<T>(item, endCount);

            var freeIndex = GetFreeSlotId();
            if (freeIndex == -1)
            {
                Debug.Log("Inventory is full!");
                return false;
            }

            _itemsVault[freeIndex] = newInvItem;

            if (rest > 0)
                return TryAddCountableItem(item, rest, out itemIndex);

            itemIndex = freeIndex;
            _countBusySlots++;
            return true;
        }
    }

    [CanBeNull]
    private InvItem<T> GetNotFilledCountableItem(Type type, out int itemIndex)
    {
        itemIndex = -1;
        for (int i = 0; i < _itemsVault.Length; i++)
        {
            var it = _itemsVault[i];
            if (it == null || it.item.GetType() != type || it.count == it.item.MaxCounts)
                continue;
            itemIndex = i;
            return it;
        }

        return null;
    }
}

[Serializable]
public class InvItem<T> where T : class, IInventoryItem
{
    public InvItem(T item, int count)
    {
        this.item = item;
        this.count = count;
        UniqueID = Guid.NewGuid();
    }

    public T item;
    public int count;
    public Guid UniqueID { get; private set; }
}