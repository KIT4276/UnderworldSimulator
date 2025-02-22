using System.Collections.Generic;
using UnityEngine;

public class InventoryFilters : MonoBehaviour
{
    [SerializeField] private InventorySystem _inventorySystem;

    public void OnDecorFilter()
    {
        var decorSlotsClone = new List<InventorySlotClone>();

        foreach (var slot in _inventorySystem.InventorySlots)
        {

            if (slot.IsOccupied && slot.Items[0] is Decor)
            {
                var strSlot = new InventorySlotClone(slot.Items[0], slot.Items.Count);
                decorSlotsClone.Add(strSlot);
            }
        }

        _inventorySystem.ClearSlots();


        foreach (var slotClone in decorSlotsClone)
        {

            foreach (var inventorySlot in _inventorySystem.InventorySlots)
            {
                if (!inventorySlot.IsOccupied)
                {
                    for (int i = 0; i < slotClone.ItemsCount; i ++)
                    {
                        inventorySlot.SetItem(slotClone.Item);
                    }
                    break;
                }
            }
        }
    }
    public void OnLootFilter()
    {

    }
}
