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
        _workbenchSystem.Exit += Escape;
        _inventorySystem.Exit += Escape;
        _lootSystem.OpenMenuAction += ToLootState;
        _lootSystem.CloseMenuAction += ToGameLoopState;

        _workbenchSystem.Destroyed += OnDestroyed;
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
        }
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        //Debug.Log("OnEscape");
        Escape();
    }

    private void OnInventory(InputAction.CallbackContext context)
    {
        if (_stateMachine.ActiveState is GameLoopState)
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
        //if (_stateMachine.ActiveState is LootState || _stateMachine.ActiveState is InventoryState)
        //{
        //    ToGameLoopState();
        //}
        /*else*/
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
