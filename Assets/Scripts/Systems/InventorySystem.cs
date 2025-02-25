using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;
    [SerializeField] private GameObject _warningSign;
    [SerializeField] private InputActionReference _escapeAction;
    private StateMachine _stateMachine;
   // private DecorHolder _decorHolder;
    private DecorationSystem _decorationSystem;

    public event Action Exit;
    public event Action ChangeSlots;
    public event Action ActivateInventoryEvent;

    public InventorySlot[] InventorySlots { get => _inventorySlot; }

    [Inject]
    public void Construct(DecorationSystem decorationSystem, /*DecorHolder decorHolder,*/ StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        //_decorHolder = decorHolder;
        _decorationSystem = decorationSystem;
        _decorationSystem.TryToRemoveDecorAction += TryReturnDecorToInventory;
        _warningSign.SetActive(false);

        // _escapeAction.action.performed += OnEscape;

        _stateMachine.ChangeStateAction += OnChangeState;
    }

    public void OnExit()
    {
        Exit?.Invoke();
    }

    public void ClearSlots()
    {
        foreach (var slot in _inventorySlot)
        {
            slot.ClearSlot();
        }
        //ChangeSlots?.Invoke();
    }

    private void OnChangeState(IExitableState state)
    {
       if(state is LootState || state is InventoryState)
        {
            this.gameObject.SetActive(true);
            ActivateInventory();
        }

    }

    public void ActivateInventory()
    {
        foreach (var slot in _inventorySlot)
        {
            slot.Initialize();
        }

        ActivateInventoryEvent?.Invoke();
        //ChangeSlots?.Invoke();
    }


    public void TryReturnLootToInventory(Item loot) /// Внимательно! Сюда обращаемся только чтобы вернуть лут
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i].IsOccupied)
            {
                if (_inventorySlot[i].GetLastItems() is Item &&
                    ((Item)_inventorySlot[i].GetLastItems()).LootType == loot.LootType)
                {
                    ReturnLootToInventory(loot, i);
                    isPlaced = true;
                    break;
                }
            }
        }
        if (!isPlaced)
        {
            for (int i = 0; i < _inventorySlot.Length; i++)
            {
                if (!_inventorySlot[i].IsOccupied)
                {
                    ReturnLootToInventory(loot, i);
                    isPlaced = true;
                    break;
                }
            }
        }

        if (!isPlaced)
        {
            Debug.Log(" не нашлось место для декора");
            StopAllCoroutines();
            _warningSign.SetActive(true);
            StartCoroutine(HideSign());
        }

    }

    private void TryReturnDecorToInventory(Decor decor)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть декор.
                                                       //для лута создать свой метод
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i].IsOccupied)
            {
                if (_inventorySlot[i].GetLastItems() is Decor
                    && ((Decor)_inventorySlot[i].GetLastItems()).DecorType == decor.DecorType)
                {
                    ReturnDecorToInventory(decor, i);
                    isPlaced = true;
                    break;
                }
            }
        }
        if (!isPlaced)
        {
            for (int i = 0; i < _inventorySlot.Length; i++)
            {
                if (!_inventorySlot[i].IsOccupied)
                {
                    ReturnDecorToInventory(decor, i);
                    isPlaced = true;
                    break;
                }
            }
        }

        if (!isPlaced)
        {
            Debug.Log(" не нашлось место для декора");
            StopAllCoroutines();
            _warningSign.SetActive(true);
            StartCoroutine(HideSign());
        }
    }

    private void ReturnDecorToInventory(Decor decor, int i)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть декор.
                                                           //для лута создать свой метод
    {
        _inventorySlot[i].SetItem(decor);
        _decorationSystem.ReturtDecorToInventory(decor);
        ChangeSlots?.Invoke();
    }

    private void ReturnLootToInventory(Item loot, int i)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть лут.
    {
        _inventorySlot[i].SetItem(loot);
        //_decorationSystem.ReturtDecorToInventory(loot);
        ChangeSlots?.Invoke();
    }

    private IEnumerator HideSign()
    {
        yield return new WaitForSeconds(3);
        _warningSign.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _warningSign.SetActive(false);
        _decorationSystem.TryToRemoveDecorAction -= TryReturnDecorToInventory;
    }
}
