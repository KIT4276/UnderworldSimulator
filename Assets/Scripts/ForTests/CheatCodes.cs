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

    public CheatCodes(InventorySystem inventory, InputActionReference test_1,
        InputActionReference test_2, InputActionReference test_3,
        InputActionReference test_4, InputActionReference test_5, Decor[] decors)
    {
        _inventory = inventory;
        _test_1 = test_1;
        _test_2 = test_2;
        _test_3 = test_3;
        _test_4 = test_4;
        _test_5 = test_5;
        _decors = decors;

        Subscribe();
    }

    private void Subscribe()
    {
        _test_1.action.started += AddAllDecor;
    }

    private void AddAllDecor(InputAction.CallbackContext context)
    {
        Debug.Log("AddAllDecor");

        for (int i = 0; i < _inventory.InventorySlots.Length; i++)
        {
            if(i < _decors.Length)
            _inventory.InventorySlots[i].SetItem(_decors[i]);
        }
    }
}
