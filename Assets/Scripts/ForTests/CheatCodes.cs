using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodes
{
    private InputActionReference _test_1;
    private InputActionReference _test_2;
    private InputActionReference _test_3;
    private InputActionReference _test_4;
    private InputActionReference _test_5;

    private readonly Decor[] _decors;
    private readonly InventorySystem _inventory;
    private readonly DrawingData _drawingData;
    private readonly GuestsStaticData _guestsData;
    private bool _drawingsInited;

    public CheatCodes(InventorySystem inventory, DrawingData drawingData, GuestsStaticData guestsData,

        InputActionReference test_1,
        InputActionReference test_2, InputActionReference test_3,
        InputActionReference test_4, InputActionReference test_5, Decor[] decors)
    {
        _inventory = inventory;
        _drawingData = drawingData;
        _guestsData = guestsData;
        _decors = decors;

        _test_1 = test_1;
        _test_2 = test_2;
        _test_3 = test_3;
        _test_4 = test_4;
        _test_5 = test_5;

        Subscribe();
    }

    private void Subscribe()
    {
        _test_1.action.started += AddAllDecor;
        _test_2.action.started += AddAllDrawings;
        _test_3.action.started += AddAllGuests;
    }

    private void AddAllGuests(InputAction.CallbackContext context)
    {
        foreach (var guest in _guestsData.Guests)
        {
            if (!guest.IsAvailable)
                guest.MakeAvailable();
        }
    }

    private void AddAllDrawings(InputAction.CallbackContext context)
    {
        if (!_drawingsInited)
        {
            foreach (var drawing in _drawingData.Drawings)
            {
                if (!drawing.IsAvailable /*!_craftSystem.AvailableDrawings.Contains(drawing)*/)
                {
                    drawing.MakeAvailable();
                }
            }
            _drawingsInited = true;
        }
    }

    private void AddAllDecor(InputAction.CallbackContext context)
    {

        for (int i = 0; i < _inventory.InventorySlots.Length; i++)
        {
            if (i < _decors.Length)
                _inventory.InventorySlots[i].SetItem(_decors[i]);
        }
    }
}
