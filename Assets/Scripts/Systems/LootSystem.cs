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
    [Inject] private MaterialsData _materials;

    private bool _isInited;
    private LootInteract _loot;
    private float _respawnTime;

    public event Action OpenMenuAction;
    public event Action CloseMenuAction;

    void Start()
    {
        _menu.SetActive(false);

        _stateMachine.ChangeStateAction += OnChangeState;
    }

    public void AllIsTacen()
    {
        bool isAll = true;

        foreach (var slot in _slots)
        {
            if (slot.Loots.Count > 0)
            {
                isAll = false;
                break;
            }
        }

        if (isAll)
        {
            OffInteractiveObject();
            OnCloseMenu();
        }
    }

    public void TakeAllLoot()
    {
        foreach (var slot in _slots)
        {
            slot.LootClickHandler.OnTakeAllClick();
        }
    }

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
        _loot.Despawn();
    }

  

    public void FillSlot(Item loot, int count, LootInteract interactiveObject, float respawnTime)
    {
        foreach (var slot in _slots)
        {
            if (!slot.IsOccupied)
            {
                for (int i = 0; i < count; i++)
                {
                    loot.Init(_materials);
                    slot.SetItem(loot);
                }
                _loot = interactiveObject;
                _respawnTime = respawnTime;
                break;
            }
        }
    }


    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
