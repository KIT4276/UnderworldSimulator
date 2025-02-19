using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private InputActionReference _escapeAction;
    [SerializeField] private GameObject _menu;
    [SerializeField] private LootSlot[] _slots;

    [Inject] private InventorySystem _inventorySystem;
    [Inject] private StateMachine _stateMachine;
    // [Inject] private LootState _lootState;

    private bool _isInited;
    private GameObject _interactiveObject;

    public event Action OpenMenuAction;
    public event Action CloseMenuAction;
    // private bool _isOnLootState;

    void Start()
    {
        _menu.SetActive(false);
        //_inventorySystem.Closed += CloseMenu;
        // _escapeAction.action.performed += OnEscape;
        //_lootState.LootStateEnter += OnLootStateEnter;
        //_lootState.LootStateExit += OnLootStateExit;

        _stateMachine.ChangeStateAction += OnChangeState;
    }

    //public void EnterLootState()
    //{
    //    //_stateMachine.Enter<LootState>();

    //}

    public void TakeAllLoot()
    {
        foreach (var slot in _slots)
        {
            slot.LootClickHandler.OnTakeAllClick();
        }
    }

    //private void OnLootStateExit()
    //{
    //    _isOnLootState = true;
    //}

    //private void OnLootStateEnter()
    //{
    //    _isOnLootState = true;
    //}

    public void OpenMenu()
    {
        _menu.SetActive(true);
        _inventorySystem.gameObject.SetActive(true);
        _inventorySystem.ActivateInventory();

        if (!_isInited)
        {
            foreach (var slot in _slots)
            {
                slot.Initialize();
            }
            _isInited = true;
        }
        OpenMenuAction?.Invoke();
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState)
            CloseMenu();
    }

    public void OnCloseMenu()
    {
        //Debug.Log(_stateMachine.ActiveState + "__________________________________");
        //if (!(_stateMachine.ActiveState is InventoryState) && !)
        CloseMenuAction?.Invoke();

    }

    private void CloseMenu()
    {
        _menu.SetActive(false);

    }

    public void TakeLootToInventory(BaseItem item)
    {
        _inventorySystem.TryReturnLootToInventory((Item)item);
    }

    public void OffInteractiveObject()
    {
        _interactiveObject.SetActive(false);
    }

    public void FillSlot(Item loot, int count, GameObject interactiveObject)
    {
        //Debug.Log(_slots[0]);
        foreach (var slot in _slots)
        {
            if (!slot.IsOccupied)
            {
                for (int i = 0; i < count; i++)
                {
                    slot.SetItem(loot);
                }
                _interactiveObject = interactiveObject;
                break;
            }
        }
    }


    //private void OnEscape(InputAction.CallbackContext context)
    //{
    //    //Debug.Log("OnEscape");
    //    CloseMenu();
    //}

    private void OnDestroy()
    {
        //_inventorySystem.Closed -= CloseMenu;
    }
}
