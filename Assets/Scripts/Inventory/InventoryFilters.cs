using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFilters : MonoBehaviour
{
    [SerializeField] private InventorySystem _inventorySystem;

    private List<InventorySlotClone> _decorSlotsClones = new();
    private List<InventorySlotClone> _lootSlotsClones = new();


    private void Start()
    {
        CreateClones();
        _inventorySystem.ChangeSlots += CreateClones;
    }

    public void CreateClones()
    {
        Debug.Log("CreateClones");
        _decorSlotsClones.Clear();
        _lootSlotsClones.Clear();

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            if (slot.IsOccupied && slot.Items[0] is Decor)
                FillList(_decorSlotsClones, slot);
            else if (slot.IsOccupied && slot.Items[0] is Item)
                FillList(_lootSlotsClones, slot);
        }
    }

    public void OnDecorFilter()
    {
        //CreateClones();
        FillSlots(_decorSlotsClones);
    }

    public void OnLootFilter()
    {
        //CreateClones();
        Debug.Log(_lootSlotsClones.Count);
        FillSlots(_lootSlotsClones);
    }


    private void FillSlots(List<InventorySlotClone> decorSlotsClone)
    {
        _inventorySystem.ClearSlots();


        foreach (var slotClone in decorSlotsClone)
        {

            foreach (var inventorySlot in _inventorySystem.InventorySlots)
            {
                if (!inventorySlot.IsOccupied)
                {
                    for (int i = 0; i < slotClone.ItemsCount; i++)
                    {
                        inventorySlot.SetItem(slotClone.Item);
                    }
                    break;
                }
            }
        }
    }

    private static void FillList(List<InventorySlotClone> decorSlotsClone, InventorySlot slot)
    {
        var slotClone = new InventorySlotClone(slot.Items[0], slot.Items.Count);
        Debug.Log(slotClone.ItemsCount);
        decorSlotsClone.Add(slotClone);
    }
}
