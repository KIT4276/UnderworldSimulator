using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatesTransitor
{
    private readonly StateMachine _stateMachine;
    private readonly DecorHolder _decorHolder;
    private readonly WorkbenchSystem _workbenchSystem;
    private readonly InventorySystem _inventorySystem;

    public StatesTransitor(StateMachine stateMachine, PlayerInput playerInput, DecorHolder decorHolder, WorkbenchSystem workbenchSystem,
        InventorySystem inventorySystem)
    {
        _stateMachine = stateMachine;
        _decorHolder = decorHolder;
        _workbenchSystem = workbenchSystem;
        _inventorySystem = inventorySystem;
        playerInput.actions["Escape"].performed += OnEscape;
        _workbenchSystem.InventoryButtonClick += ToDecorateState;
        _workbenchSystem.Exit += ToGameLoopState;
        _inventorySystem.Exit += ConditionalToWorkbenchState;
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
        }
    }

    private void ConditionalToWorkbenchState()
    {
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
