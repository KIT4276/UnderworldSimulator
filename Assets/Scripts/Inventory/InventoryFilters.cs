using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFilters : MonoBehaviour
{
    [SerializeField] private FilterButtonSwitch _filterButtonSwitch;
    [Space]
    [SerializeField] private InventorySystem _inventorySystem;

    private List<InventorySlotClone> _decorSlotsClones = new();
    private List<InventorySlotClone> _lootSlotsClones = new();


    private void Start()
    {
        CreateClones();
        _inventorySystem.ChangeSlots += CreateClones;
       
        _filterButtonSwitch.Switch(FilterType.NoFilter);
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

    public void OnNoFilter()
    {
        FillSlots(_decorSlotsClones);
        FillSlots(_lootSlotsClones);
        //todo FillSlots(_questsSlotsClones);
        _filterButtonSwitch.Switch(FilterType.NoFilter);
    }

    public void OnDecorFilter()
    {
        _inventorySystem.ClearSlots();
        FillSlots(_decorSlotsClones);
        _filterButtonSwitch.Switch(FilterType.Decor);
    }

    public void OnCraftFilter()
    {
        //Debug.Log(_lootSlotsClones.Count);
        _inventorySystem.ClearSlots();
        FillSlots(_lootSlotsClones);
        _filterButtonSwitch.Switch(FilterType.Craft);
    }

    public void OnQuestsFilter()
    {
        _inventorySystem.ClearSlots();
        FillSlots(_lootSlotsClones);
        _filterButtonSwitch.Switch(FilterType.Quests);
    }

    private void FillSlots(List<InventorySlotClone> decorSlotsClone)
    {
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
        //Debug.Log(slotClone.ItemsCount);
        decorSlotsClone.Add(slotClone);
    }
}
