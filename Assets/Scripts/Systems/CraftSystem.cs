using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem
{
    //private Drawing _activeDrawing;
    private InventorySystem _inventorySystem;
    private readonly DrawingData _drawingDatas;

    public Drawing ActiveDrawing { get; private set; }
    public List<Drawing> AvailableDrawings { get; private set; }
    public int Count { get; private set; }

    public event Action ChangeCount;
    public event Action Crafted;
    public event Action DrawingAdded;
    public event Action NotEnoughMaterials; 

    private List<CraftItem> _availableMaterials = new();

    public CraftSystem(InventorySystem inventorySystem, DrawingData drawingDatas)
    {
        AvailableDrawings = new();

        foreach (var draw in drawingDatas.Drawings)
        {
            draw.BecameAvailable += OnBecameAvailable;
        }

        Count = 1;
        _inventorySystem = inventorySystem;
        _drawingDatas = drawingDatas;
    }

    public void OnDestroy()
    {
        foreach(var draw in _drawingDatas.Drawings)
        {
            draw.MakeUnavailable();
        }
    }

    private void OnBecameAvailable(BaseHandledReward reward)
    {
        if(AvailableDrawings.Count == 0)
        {
            ActiveDrawing = (Drawing)reward;
        }
        AvailableDrawings.Add((Drawing)reward);
       // Debug.Log(AvailableDrawings.Count);
        DrawingAdded?.Invoke();
    }

    public void AwakeMenu()
    {
        Count = 1;
        //Debug.Log(AvailableDrawings.Count);
    }

    public void SelectDrawing(Drawing drawing)
    {
        ActiveDrawing = drawing;
        Count = 1;
        ChangeCount?.Invoke();
       // Debug.Log("SelectDrawing");
    }

    public void OnChangeCount(int count)
    {
        Count += count;
        if (Count < 1)
            Count = 1;

        ChangeCount?.Invoke();
        //Debug.Log("OnChangeCount");
    }

    public void CreateDecor()
    {
        if (EnoughMaterials())// TODO && EnoughSlots()!)
        {
            for (int i = 0; i < Count; i++)
            {
                // var decor = _decoratorFactory.SpawnDecor(_activeDrawing.Decor);// 

                _inventorySystem.TryReturnDecorToInventory(ActiveDrawing.Decor); // temporary solution!

                foreach (var mat in ActiveDrawing.DrawingComponents)
                {
                    for (int j = 0; j < mat.Count; j++)
                    {
                        _inventorySystem.RemoveItems(mat.Material);
                    }
                }
            }
        }
        else
        {
            NotEnoughMaterials?.Invoke();
        }

        Count = 1;
        Crafted?.Invoke();
       // Debug.Log("Crafted");
    }

    private bool EnoughMaterials()
    {
        FillAllAvalibaleMaterials();

        foreach (var mat in ActiveDrawing.DrawingComponents)
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
