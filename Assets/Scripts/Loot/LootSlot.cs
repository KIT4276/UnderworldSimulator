using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LootSlot : InventorySlot
{
    [Inject] private InventorySystem _inventorySystem;

    public List<BaseItem> Loots { get => Items; }

    private void Awake()
    {
        
    }
}
