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
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        Debug.Log("OnEscape");

        switch (_stateMachine.ActiveState)
        {
            case DecorationState:
                if (_decorHolder.ActiveDecor == null)
                {
                    _stateMachine.Enter<WorkbenchState>();
                }
                else
                {
                    _workbenchSystem.ShowSign();
                }
                break;
            case WorkbenchState:
                _stateMachine.Enter<GameLoopState>();
                break;
        }
    }
}
