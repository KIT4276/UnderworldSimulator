using System.Collections.Generic;
using UnityEngine;

public class InventoryFilters : MonoBehaviour
{
    [SerializeField] private FilterButtonSwitch _filterButtonSwitch;
    [Space]
    [SerializeField] private InventorySystem _inventorySystem;

    private List<InventorySlotClone> _decorSlotsClones = new();
    private List<InventorySlotClone> _craftSlotsClones = new();
    private List<InventorySlotClone> _questsSlotsClones = new();


    private void Start()
    {
        CreateAllClones();
        // _inventorySystem.ChangeSlots += CreateClones;

        _inventorySystem.ChangeDecorSlots += CreateDecorsClones;
        _inventorySystem.ChangeCraftItemSlots += CreateCraftItemClones;
        _inventorySystem.ChangeQuestsItemSlots += CreateQuestsItemClones;

        _inventorySystem.ActivateInventoryEvent += OnActivateInventory;

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            // slot.ChangeCount += CreateAllClones;
            slot.ChangeDecorCount += CreateDecorsClones;
            slot.ChangeCraftCount += CreateCraftItemClones;
            slot.ChangeCraftCount += CreateQuestsItemClones;
        }

        _filterButtonSwitch.Switch(FilterType.NoFilter);
    }


    private void CreateDecorsClones()
    {
        _decorSlotsClones.Clear();

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            if (slot.IsOccupied && slot.Items[0] is Decor)
                FillList(_decorSlotsClones, slot);
        }
        
    }

    private void CreateCraftItemClones()
    {
        _craftSlotsClones.Clear();

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            if (slot.IsOccupied && slot.Items[0] is CraftItem)
                FillList(_craftSlotsClones, slot);
        }
    }

    private void CreateQuestsItemClones()
    {
        _questsSlotsClones.Clear();

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            if (slot.IsOccupied && slot.Items[0] is QuestsItem)
                FillList(_questsSlotsClones, slot);
        }
    }


    public void CreateAllClones()
    {
      //  Debug.Log("CreateAllClones");

        _decorSlotsClones.Clear();
        _craftSlotsClones.Clear();
        _questsSlotsClones.Clear();

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            if (slot.IsOccupied && slot.Items[0] is Decor)
                FillList(_decorSlotsClones, slot);
            else if (slot.IsOccupied && slot.Items[0] is CraftItem)
                FillList(_craftSlotsClones, slot);
            else if (slot.IsOccupied && slot.Items[0] is QuestsItem)
                FillList(_questsSlotsClones, slot);
        }
    }

    public void OnNoFilter()
    {
        _inventorySystem.ClearSlots();
        FillSlots(_decorSlotsClones);
        FillSlots(_craftSlotsClones);
        FillSlots(_questsSlotsClones);
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
        _inventorySystem.ClearSlots();
        FillSlots(_craftSlotsClones);
        _filterButtonSwitch.Switch(FilterType.Craft);
    }

    public void OnQuestsFilter()
    {
        _inventorySystem.ClearSlots();
        FillSlots(_questsSlotsClones);
        _filterButtonSwitch.Switch(FilterType.Quests);
    }
    private void OnActivateInventory()
    {
        OnNoFilter();
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
        decorSlotsClone.Add(slotClone);
    }

    private void OnDestroy()
    {
        _inventorySystem.ChangeDecorSlots -= CreateDecorsClones;
        _inventorySystem.ChangeCraftItemSlots -= CreateCraftItemClones;
        _inventorySystem.ChangeQuestsItemSlots -= CreateQuestsItemClones;

        _inventorySystem.ActivateInventoryEvent -= OnActivateInventory;

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            slot.ChangeDecorCount += CreateDecorsClones;
            slot.ChangeCraftCount += CreateCraftItemClones;
            slot.ChangeCraftCount += CreateQuestsItemClones;
        }
    }
}
