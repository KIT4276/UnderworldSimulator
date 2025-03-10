using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LootClickHandler), typeof (SlotEnterChangeImage))]
public class LootSlot : InventorySlot
{
    [SerializeField] private LootClickHandler _lootClickHandler;

    public List<BaseItem> Loots { get => Items; }
    public LootClickHandler LootClickHandler { get => _lootClickHandler; } 
}
