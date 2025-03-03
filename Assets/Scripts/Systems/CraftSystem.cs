using System;
using Unity.VisualScripting;
using UnityEngine;

public class CraftSystem
{
    public int Count {get; private set;}

    private Drawing _activeDrawing;
    private InventorySystem _inventorySystem;
    private DrawingData _drawingDatas;

    public DrawingData DrawingDatas { get => _drawingDatas; }


    public event Action<Drawing> Created;

    public CraftSystem(InventorySystem inventorySystem, DrawingData drawingDatas)
    {
        _inventorySystem = inventorySystem;
        _drawingDatas = drawingDatas;

        _activeDrawing = _drawingDatas.Drawings[0];
        Created?.Invoke(_activeDrawing);
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
