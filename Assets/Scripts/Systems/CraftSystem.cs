using System;
using System.Collections.Generic;
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
    private List<CraftItem> _uzedMaterials = new();

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
        //todo check vfterials!
        _uzedMaterials.Clear();

        if (EnoughMaterials())
        {
            for (int i = 0; i < Count; i++)
            {
                var decor = _decoratorFactory.SpawnDecor(_activeDrawing.Decor);

                _inventorySystem.TryReturnDecorToInventory(decor);
            }
            foreach (var mat in _uzedMaterials)
            {
                _inventorySystem.RemoveItems(mat/*, Count*/);//Count&//
            }
        }
        else
        {
            Debug.Log("недостаточно материалов!");
            _uzedMaterials.Clear();
        }
    }

    private bool EnoughMaterials()
    {
        FillAllAvalibaleMaterials();

        foreach (var mat in _activeDrawing.DrawingComponents)
        {
            if (mat.Count * Count >= TakeMaterials(mat.Material))
            {
                return false;
            }
        }
        return true;
    }
    /// //////////////////////////////////////////////////////TODO real take materials from _inventorySystem.InventorySlots.Items!
    private int TakeMaterials(LootType type)
    {
        int i = 0;
        foreach (var mat in _availableMaterials)
        {
            if (mat.LootType == type)
            {
                //_availableMaterials.Remove(mat);
                _uzedMaterials.Add(mat);
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
