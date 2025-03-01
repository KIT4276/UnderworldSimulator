using System;
using UnityEngine;

public class CraftSystem
{
    public int Count {get; private set;}

    private Drawing _activeDrawing;
    private InventorySystem _inventorySystem;

    public CraftSystem(InventorySystem inventorySystem)
    {
        _inventorySystem = inventorySystem;
    }

    public void SelectDrawing(Drawing drawing)
    {
        _activeDrawing = drawing;
    }

    public void ChangeCount(int count)
    {
        Count += count;
        if (Count < 0)
            Count = 0;
    }

    public void Create()
    {
        //TODO with _activeDrawing and place to _inventorySystem
    }
}
