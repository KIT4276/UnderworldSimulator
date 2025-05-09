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
    private readonly PlayerInput _playerInput;

    public event Action EscapeGame;

    public StatesTransitor(StateMachine stateMachine, PlayerInput playerInput, DecorHolder decorHolder, WorkbenchSystem workbenchSystem,
        InventorySystem inventorySystem, LootSystem lootSystem)
    {
        _stateMachine = stateMachine;
        _decorHolder = decorHolder;
        _workbenchSystem = workbenchSystem;
        _inventorySystem = inventorySystem;
        _lootSystem = lootSystem;
        _playerInput = playerInput;

        playerInput.actions["Escape"].performed += OnEscape;
        playerInput.actions["Inventory"].performed += OnInventory;
        _workbenchSystem.InventoryButtonClick += ToDecorateState;
        _workbenchSystem.CraftButtonClick += ConditionalToCraftState;

        _workbenchSystem.Exit += Escape;
        _inventorySystem.Exit += Escape;
        _lootSystem.OpenMenuAction += ToLootState;
        _lootSystem.CloseMenuAction += ToGameLoopState;

        _workbenchSystem.Destroyed += OnDestroyed;
    }

    public void ConditionalToInventoryState()
    {
        if (_stateMachine.ActiveState is GameLoopState || _stateMachine.ActiveState is PseudoCraftState)
            ToInventoryState();
    }


    public void ToPseudoCraft()
    {
        _stateMachine.Enter<PseudoCraftState>();
    }

    private void Escape()
    {
        //Debug.Log("Escape");
        //Debug.Log(_stateMachine.ActiveState);

        switch (_stateMachine.ActiveState)
        {
            case DecorationState:
                ConditionalToWorkbenchState();
                //ToWorkbenchState();
                break;
            case WorkbenchState:
                //ToGameLoopState();
                ConditionalToGameLoopState();
                break;
            case InventoryState:
                ToGameLoopState();
                break;
            case LootState:
                ToGameLoopState();
                break;
            case CraftState:
                ToWorkbenchState();
                break;
            case GameLoopState:
                EscapeGame?.Invoke();
                break;
            case PseudoCraftState:
                ToGameLoopState();
                break;
        }
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        Escape();
    }

    private void OnInventory(InputAction.CallbackContext context)
    {
        if (_stateMachine.ActiveState is GameLoopState || _stateMachine.ActiveState is PseudoCraftState)
            ToInventoryState();
    }

    private void ConditionalToGameLoopState()
    {
        if (_decorHolder.ActiveDecor == null)
        {
            ToGameLoopState();
        }
        else
        {
            Debug.Log("else");
            _workbenchSystem.ShowSign();
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
            Debug.Log("else");
            _workbenchSystem.ShowSign();
        }
    }

    private void ConditionalToCraftState()
    {
        if (_decorHolder.ActiveDecor == null)
        {
            ToCraftState();
        }
        else
        {
            Debug.Log("else");
            _workbenchSystem.ShowSign();
        }
    }


    private void ToCraftState()
    {
        _stateMachine.Enter<CraftState>();
    }

    public void ToDecorateState()
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

    private void OnDestroyed()
    {
        _playerInput.actions["Escape"].performed -= OnEscape;
        _playerInput.actions["Inventory"].performed -= OnInventory;
        _workbenchSystem.InventoryButtonClick -= ToDecorateState;
        _workbenchSystem.Exit -= Escape;
        _inventorySystem.Exit -= Escape;
        _lootSystem.OpenMenuAction -= ToLootState;
        _lootSystem.CloseMenuAction -= ToGameLoopState;
        _workbenchSystem.Destroyed -= OnDestroyed;
    }

}
