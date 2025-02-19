using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatesTransitor
{
    private readonly StateMachine _stateMachine;
    private readonly DecorHolder _decorHolder;
    private readonly WorkbenchSystem _workbenchSystem;
    private readonly InventorySystem _inventorySystem;
    private readonly LootSystem _lootSystem;

    public StatesTransitor(StateMachine stateMachine, PlayerInput playerInput, DecorHolder decorHolder, WorkbenchSystem workbenchSystem,
        InventorySystem inventorySystem, LootSystem lootSystem)
    {
        _stateMachine = stateMachine;
        _decorHolder = decorHolder;
        _workbenchSystem = workbenchSystem;
        _inventorySystem = inventorySystem;
        _lootSystem = lootSystem;

        playerInput.actions["Escape"].performed += OnEscape;
        playerInput.actions["Inventory"].performed += OnInventory;
        _workbenchSystem.InventoryButtonClick += ToDecorateState;
        _workbenchSystem.Exit += ToGameLoopState;
        _inventorySystem.Exit += ConditionalToWorkbenchState;
        _lootSystem.OpenMenuAction += ToLootState;
        _lootSystem.CloseMenuAction += ToGameLoopState;
    }


    private void OnEscape(InputAction.CallbackContext context)
    {

        switch (_stateMachine.ActiveState)
        {
            case DecorationState:
                ConditionalToWorkbenchState();
                break;
            case WorkbenchState:
                ToGameLoopState();
                break;
            case InventoryState:
                ToGameLoopState();
                break;
            case LootState:
                ToGameLoopState();
                break;
        }
    }

    private void OnInventory(InputAction.CallbackContext context)
    {
        ToInventoryState();
    }

    private void ConditionalToWorkbenchState()
    {
        if (_stateMachine.ActiveState is LootState) return; 

        if (_decorHolder.ActiveDecor == null)
        {
            ToWorkbenchState();
        }
        else
        {
            _workbenchSystem.ShowSign();
        }
    }

    private void ToDecorateState()
    {
        _stateMachine.Enter<DecorationState>();
    }

    private void ToWorkbenchState()
    {
        _stateMachine.Enter<WorkbenchState>();
    }

    private void ToGameLoopState()
    {
        _stateMachine.Enter<GameLoopState>();
    }

    private void ToLootState()
    {
        _stateMachine.Enter<LootState>();
    }

    private void ToInventoryState()
    {
        _stateMachine.Enter<InventoryState>();
    }
}
