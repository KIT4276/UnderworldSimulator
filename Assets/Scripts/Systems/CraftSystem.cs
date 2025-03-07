using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Zenject;

public class CraftSystem
{
    public int Count { get; private set; }

    private Drawing _activeDrawing;
    private InventorySystem _inventorySystem;
    private DrawingData _drawingDatas;

    public DrawingData DrawingDatas { get => _drawingDatas; }
    public Drawing ActiveDrawing { get => _activeDrawing; }

    [Inject] private DecorFactory _decoratorFactory;

    public event Action ChangeCount;

    private List<CraftItem> _availableMaterials = new();

    public CraftSystem(InventorySystem inventorySystem, DrawingData drawingDatas)
    {
        Count = 1;
        _inventorySystem = inventorySystem;
        _drawingDatas = drawingDatas;

        _activeDrawing = _drawingDatas.Drawings[0];
    }

    public void SelectDrawing(Drawing drawing)
    {
        _activeDrawing = drawing;
        Count = 1;
        ChangeCount?.Invoke();
    }

    public void OnChangeCount(int count)
    {
        Count += count;
        if (Count < 0)
            Count = 0;

        ChangeCount?.Invoke();
    }

    public void CreateDecor()
    {
        if (EnoughMaterials())// TODO && EnoughSlots()!)
        {
            for (int i = 0; i < Count; i++)
            {
                // var decor = _decoratorFactory.SpawnDecor(_activeDrawing.Decor);// 

                _inventorySystem.TryReturnDecorToInventory(_activeDrawing.Decor); // temporary solution!

                foreach (var mat in _activeDrawing.DrawingComponents)
                {
                    for(int j = 0; j < mat.Count; j++)
                    {
                        _inventorySystem.RemoveItems(mat.Material);
                    }
                }
            }
        }
        else
        {
            Debug.Log("недостаточно материалов!");
        }
    }

    private bool EnoughMaterials()
    {
        FillAllAvalibaleMaterials();

        foreach (var mat in _activeDrawing.DrawingComponents)
        {
            if (mat.Count * Count > TakeMaterials(mat.Material))
            {
                return false;
            }
        }
        return true;
    }

    private int TakeMaterials(LootType type)
    {
        int i = 0;
        foreach (var mat in _availableMaterials)
        {
            if (mat.LootType == type)
            {
                i++;
            }
        }

        return i;
    }

    private void FillAllAvalibaleMaterials()
    {
        _availableMaterials.Clear();

        foreach (var slot in _inventorySystem.InventorySlots)
        {
            if (slot.IsOccupied)
            {
                foreach (var item in slot.Items)
                {
                    if (item is CraftItem)
                        _availableMaterials.Add((CraftItem)item);
                }
            }
        }
    }
}
