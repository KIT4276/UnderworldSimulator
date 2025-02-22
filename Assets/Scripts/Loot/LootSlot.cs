using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LootClickHandler), typeof (ButtonEnterChangeImage))]
public class LootSlot : InventorySlot
{
    [SerializeField] private LootClickHandler _lootClickHandler;

    public List<BaseItem> Loots { get => Items; }
    public LootClickHandler LootClickHandler { get => _lootClickHandler; } 
}
