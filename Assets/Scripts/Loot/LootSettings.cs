using System;
using UnityEngine;

[Serializable]
public abstract class LootSettings /*: MonoBehaviour*/
{
    //[SerializeField] private Item _loot;
    [SerializeField] protected int _count;


    public abstract Item Loot { get; }
    public int Count { get => _count; }
}

[Serializable]
public class CraftLootSettings : LootSettings
{
    [SerializeField] private CraftItem _loot;
    //[SerializeField] private int _count;

    public override Item Loot => _loot;

    //public int Count => _count;
}

[Serializable]
public class QuestsLootSettings : LootSettings
{
    [SerializeField] private QuestsItem _loot;
   // [SerializeField] private int _count;

    public override Item Loot => _loot;

   // public int Count => _count;
}
