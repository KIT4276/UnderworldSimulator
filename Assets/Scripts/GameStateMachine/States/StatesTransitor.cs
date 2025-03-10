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
    private readonly CraftSystem _craftSystem;

    public StatesTransitor(StateMachine stateMachine, PlayerInput playerInput, DecorHolder decorHolder, WorkbenchSystem workbenchSystem,
        InventorySystem inventorySystem, LootSystem lootSystem, CraftSystem craftSystem)
    {
        _stateMachine = stateMachine;
        _decorHolder = decorHolder;
        _workbenchSystem = workbenchSystem;
        _inventorySystem = inventorySystem;
        _lootSystem = lootSystem;
        _playerInput = playerInput;
        _craftSystem = craftSystem;

        playerInput.actions["Escape"].performed += OnEscape;
        playerInput.actions["Inventory"].performed += OnInventory;
        _workbenchSystem.InventoryButtonClick += ToDecorateState;
        _workbenchSystem.CraftButtonClick += ToCraftState;

        _workbenchSystem.Exit += Escape;
        _inventorySystem.Exit += Escape;
        _lootSystem.OpenMenuAction += ToLootState;
        _lootSystem.CloseMenuAction += ToGameLoopState;
        _craftSystem.EscapeAction += ToWorkbenchState;

        _workbenchSystem.Destroyed += OnDestroyed;
    }

    private void Escape()
    {
        switch (_stateMachine.ActiveState)
        {
            case DecorationState:
                ConditionalToWorkbenchState();
                break;
            case WorkbenchState:
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
        }
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
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
            _workbenchSystem.ShowSign();
        }
    }


    private void ToCraftState()
    {
        _stateMachine.Enter<CraftState>();
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
        _craftSystem.EscapeAction -= ToWorkbenchState;
    }
}
