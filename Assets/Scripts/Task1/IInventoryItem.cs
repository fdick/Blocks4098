using System;

public interface IInventoryItem
{
    public bool IsCountable { get; }
    public int MaxCounts { get; }
}