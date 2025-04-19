using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private GameObject _warningSign;

    private StateMachine _stateMachine;
    private DecorationSystem _decorationSystem;

    public event Action Exit;
    public event Action ActivateInventoryEvent;

    public event Action ChangeDecorSlots;
    public event Action ChangeCraftItemSlots;
    public event Action ChangeQuestsItemSlots;

    public InventorySlot[] InventorySlots { get => _inventorySlots; }

    private InventoryHolder _inventoryHolder;


    [Inject]
    public void Construct(DecorationSystem decorationSystem, StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _decorationSystem = decorationSystem;
        _decorationSystem.TryToRemoveDecorAction += TryReturnDecorToInventory;
        _warningSign.SetActive(false);
        _stateMachine.ChangeStateAction += StateChanged;

        _inventoryHolder = new();
    }

    public void RemoveItems(LootType lootType)
    {
        foreach (var slot in _inventorySlots)
        {
            if (slot.IsOccupied &&
                slot.GetLastItems() is CraftItem &&
                ((CraftItem)slot.GetLastItems()).LootType == lootType)
            {
                var item = slot.TakeLastItem();
                _inventoryHolder.Remove(item);
                break;
            }
        }
    }

    public void OnExit()
    {
        Exit?.Invoke();
    }

    public void ClearSlots()
    {
        foreach (var slot in _inventorySlots)
        {
            slot.ClearSlot();
        }
    }

    public void ActivateInventory()
    {
        foreach (var slot in _inventorySlots)
        {
            slot.Initialize();
        }

        ActivateInventoryEvent?.Invoke();
    }

    public void TryReturnLootToInventory(Item loot) /// Внимательно! Сюда обращаемся только чтобы вернуть лут
    {
        if (_inventorySlots.Length == 0)
        {
            Debug.LogWarning("The links to the slots have disappeared!");
            return;
        }

        bool isPlaced = false;

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].IsOccupied)
            {
                if (_inventorySlots[i].GetLastItems() is Item &&
                    ((Item)_inventorySlots[i].GetLastItems()).LootType == loot.LootType)
                {
                    ReturnLootToInventory(loot, i);
                    isPlaced = true;
                    break;
                }
            }
        }
        if (!isPlaced)
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                if (!_inventorySlots[i].IsOccupied)
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

    public void TryReturnDecorToInventory(Decor decor)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть декор.
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].IsOccupied)
            {
                if (_inventorySlots[i].GetLastItems() is Decor
                    && ((Decor)_inventorySlots[i].GetLastItems()).DecorType == decor.DecorType)
                {
                    ReturnDecorToInventory(decor, i);

                    isPlaced = true;
                    break;
                }
            }
        }
        if (!isPlaced)
        {
            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                if (!_inventorySlots[i].IsOccupied)
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

    public int CalculateAvailableMaterial(LootType material)
    {
        var count = _inventoryHolder.CalculateMaterial(material);
        return count;
    }

    public void FillDecorItems()
    {
        if (_inventoryHolder.AllItemsInInventory == null || _inventoryHolder.AllItemsInInventory.Count == 0) return;
        
        foreach (var item in _inventoryHolder.AllItemsInInventory)
        {
            if (item is Decor)
            {
                FindPlaceForItem(item);
            }
        }
    }

    public void FillCraftItems()
    {
        if (_inventoryHolder.AllItemsInInventory == null || _inventoryHolder.AllItemsInInventory.Count == 0) return;

        foreach (var item in _inventoryHolder.AllItemsInInventory)
        {
            if (item is CraftItem)
            {
                FindPlaceForItem(item);
            }
        }
    }

    private void StateChanged(IExitableState state)
    {
        if (state is LootState || state is InventoryState || state is CraftState) //TODO
        {
            this.gameObject.SetActive(true);
            ActivateInventory();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void ReturnDecorToInventory(Decor decor, int i)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть декор.
                                                           //для лута создать свой метод
    {
        _inventorySlots[i].SetItem(decor);
        _decorationSystem.ReturtDecorToInventory(decor);
        _inventoryHolder.Add(decor);
        ChangeDecorSlots?.Invoke();

    }

    private void ReturnLootToInventory(Item loot, int i)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть лут.
    {
        _inventorySlots[i].SetItem(loot);

        if (loot is CraftItem)
        {
            ChangeCraftItemSlots?.Invoke();
        }
        else if (loot is QuestsItem)
        {
            ChangeQuestsItemSlots?.Invoke();
        }
        _inventoryHolder.Add(loot);
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
    }


    private void OnDestroy()
    {
        _decorationSystem.TryToRemoveDecorAction -= TryReturnDecorToInventory;
    }


    private bool FindPlaceForItem(IBaseItem item)
    {
        foreach (var slot in InventorySlots)
        {
            if (!slot.IsOccupied || (slot.IsOccupied && slot.Items[0].GetIcon() == item.GetIcon()))// костылище пока что
            {
                slot.SetItem(item);
                return true;
            }
        }
        return false;
    }
}

public class InventoryHolder
{
    public List<IBaseItem> AllItemsInInventory { get; private set; }

    public event Action Change;

    public void Add(IBaseItem item)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();


        AllItemsInInventory.Add(item);

        Change?.Invoke();
    }

    public void Remove(IBaseItem item)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        AllItemsInInventory.Remove(item);

        Change?.Invoke();
    }

    public int CalculateMaterial(LootType material)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        int count = 0;

        foreach (IBaseItem item in AllItemsInInventory)
        {
            if (item is Item)
            {
                if (((Item)item).LootType == material)
                {
                    count++;
                }
            }
        }

        return count;
    }
}

