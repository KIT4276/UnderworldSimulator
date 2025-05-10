using System;
using System.Collections.Generic;

public class InventoryHolder : ISavedProgress
{

    public List<IBaseItem> AllItemsInInventory { get; private set; }

    public event Action Change;
    public event Action LoasSave;

    private MaterialsData _materialsData;
    private readonly InventorySystem _inventorySystem;

    public InventoryHolder(MaterialsData materialsData, InventorySystem inventorySystem)
    {
        _materialsData = materialsData;
        _inventorySystem = inventorySystem;
    }

    public void Add(IBaseItem item)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        //Debug.Log("Add AllItemsInInventory");
        AllItemsInInventory.Add(item);

        Change?.Invoke();
    }

    public void RemoveDecor(Decor decor)
    {
        for(var i = 0;  i < AllItemsInInventory.Count; i++) 
        
        {
            if (AllItemsInInventory[i] is Decor decorInInvent)
            {
                if(decorInInvent.DecorType == decor.DecorType)
                {
                    Remove(AllItemsInInventory[i]);
                }
            }
        }
    }

    private void Remove(IBaseItem item)
    {
        AllItemsInInventory.Remove(item);
        //_inventorySystem.UpdateSlots();
        Change?.Invoke();
    }

    public void RemoveByType(LootType lootType)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        for (var i = 0; i < AllItemsInInventory.Count; i++)
        {
            if (AllItemsInInventory[i] != null && AllItemsInInventory[i] is Item mat)
            {
                if (mat.LootType == lootType)
                {
                    Remove(mat);
                    break;
                }
            }
        }
    }

    public int CalculateMaterial(LootType material)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        int count = 0;

        foreach (IBaseItem baseItem in AllItemsInInventory)
        {
            if (baseItem is Item item)
            {
                if (item.LootType == material)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void SaveProgress(PlayerProgress progress)
    {
        //Debug.Log("SaveProgress");
        if (progress.InventoryItems != null)
        {

            foreach (var inventoryItem in AllItemsInInventory)
            {
                if (inventoryItem is Item item)
                {

                    progress.InventoryItems.Add(item);
                }
            }

        }
        if (progress.InventoryDecors != null)
        {
            foreach (var inventoryItem in AllItemsInInventory)
            {
                if (inventoryItem is Decor decor)
                {
                    progress.InventoryDecors.Add(decor);
                }
            }
        }
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        if (progress != null)
        {
            foreach (var item in progress.InventoryItems)
            {
                AllItemsInInventory.Add(item);
                item.Init(_materialsData);
            }
            foreach (var decor in progress.InventoryDecors)
            {
                AllItemsInInventory.Add(decor);
            }
        }
        LoasSave?.Invoke();
    }
}

