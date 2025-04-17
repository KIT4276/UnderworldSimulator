using System;
using UnityEngine;

[Serializable]
public abstract class LootSettings 
{
    [SerializeField] protected int _count;

    public abstract Item Loot { get; }
    public int Count { get => _count; }
}

[Serializable]
public class CraftLootSettings : LootSettings
{
    [SerializeField] private CraftItem _loot;

    public int CurrentCount { get; private set; }
    public override Item Loot => _loot;
    
    public void Init()
    {
        CurrentCount = _count;
    }

    public void DecreaseCount(int value)
    {
        CurrentCount -= value;
    }

    public void RestartCount()
    {
        CurrentCount = _count;
    }
}

[Serializable]
public class QuestsLootSettings : LootSettings
{
    [SerializeField] private QuestsItem _loot;

    public override Item Loot => _loot;
}
