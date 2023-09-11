using System;
using UnityEngine;

public abstract class ItemBase : ScriptableObject, IInventoryItem
{
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public bool IsCountable { get; private set; }
    [field: SerializeField] public int MaxCounts { get; private set; } = 1;
    [SerializeField] protected Sprite _inventoryImage;


    public Sprite InventoryImage => _inventoryImage;
}