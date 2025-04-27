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
    private InventoryHolder _inventoryHolder;

    public event Action Exit;
    public event Action ActivateInventoryEvent;

    public event Action ChangeDecorSlots;
    public event Action ChangeCraftItemSlots;
    public event Action ChangeQuestsItemSlots;

    public InventorySlot[] InventorySlots { get => _inventorySlots; }

    public InventoryHolder InventoryHolder { get => _inventoryHolder; }


    [Inject]
    public void Construct(DecorationSystem decorationSystem, StateMachine stateMachine, MaterialsData materialsData)
    {
        _stateMachine = stateMachine;
        _decorationSystem = decorationSystem;

        _decorationSystem.TryToRemoveDecorAction += TryReturnDecorToInventory;
        _warningSign.SetActive(false);
        _stateMachine.ChangeStateAction += StateChanged;

        _inventoryHolder = new(materialsData);

        _inventoryHolder.LoasSave += UpdateSlots;
    }


    private void UpdateSlots()
    {
        FillDecorItems();
        FillCraftItems();
    }

    public void RemoveItems(LootType lootType)
    {
        Debug.Log("RemoveItems");

        _inventoryHolder.RemoveByType(lootType);
        ClearSlots();
        FillCraftItems();
        FillDecorItems();
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
        // Debug.Log("ActivateInventory");
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
        //Debug.Log("TryReturnDecorToInventory");

        bool isPlaced = false;

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].IsOccupied)
            {
                if (_inventorySlots[i].GetLastItems() is Decor inventDecor
                    && /*((Decor)_inventorySlots[i].GetLastItems())*/inventDecor.DecorType == decor.DecorType)
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
                //Debug.Log("FillDecorItems");
                FindPlaceForItem(item);
            }
        }
    }

    public void FillCraftItems()
    {
        if (_inventoryHolder.AllItemsInInventory == null || _inventoryHolder.AllItemsInInventory.Count == 0) return;

        foreach (var item in _inventoryHolder.AllItemsInInventory)
        {
            if (item is Item)
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
        //Debug.Log("ReturnDecorToInventory");
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
        _inventoryHolder.Change -= UpdateSlots;
    }


    private bool FindPlaceForItem(IBaseItem item)
    {

        foreach (var slot in InventorySlots)
        {
            if (!slot.IsOccupied || (slot.IsOccupied && slot.Items[0].GetIcon() == item.GetIcon()))// костылище пока что
            {
                //Debug.Log("FindPlaceForItem");
                slot.SetItem(item);
                return true;
            }
        }
        return false;
    }
}

public class InventoryHolder : ISavedProgress
{

    public List<IBaseItem> AllItemsInInventory { get; private set; }

    public event Action Change;
    public event Action LoasSave;

    private MaterialsData _materialsData;

    public InventoryHolder(MaterialsData materialsData)
    {
        _materialsData = materialsData;
    }

    public void Add(IBaseItem item)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        Debug.Log("Add AllItemsInInventory");
        AllItemsInInventory.Add(item);

        Change?.Invoke();
    }

    private void Remove(IBaseItem item)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        AllItemsInInventory.Remove(item);

        Change?.Invoke();
    }

    public void RemoveByType(LootType lootType)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        // foreach (var item in AllItemsInInventory)
        for (var i = 0; i < AllItemsInInventory.Count; i++)
        {
            if (AllItemsInInventory[i] != null && AllItemsInInventory[i] is Item mat)
            {
                if (mat.LootType == lootType)
                {
                    Remove(mat);
                }
            }
        }
        Debug.Log(AllItemsInInventory.Count);
        if(AllItemsInInventory.Count == 1)
        {
            Debug.Log(AllItemsInInventory[0]);
        }
        
    }

    public int CalculateMaterial(LootType material)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        int count = 0;

        foreach (IBaseItem baseItem in AllItemsInInventory)
        {
            if (baseItem is Item item)
            {
                if (item.LootType == material)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void SaveProgress(PlayerProgress progress)
    {
        //Debug.Log("SaveProgress");
        if (progress.InventoryItems != null)
        {

            foreach (var inventoryItem in AllItemsInInventory)
            {
                if (inventoryItem is Item item)
                {

                    progress.InventoryItems.Add(item);
                }
            }

        }
        if (progress.InventoryDecors != null)
        {
            foreach (var inventoryItem in AllItemsInInventory)
            {
                if (inventoryItem is Decor decor)
                {
                    progress.InventoryDecors.Add(decor);
                }
            }
        }
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (AllItemsInInventory == null)
            AllItemsInInventory = new();

        if (progress != null)
        {
            foreach (var item in progress.InventoryItems)
            {
                AllItemsInInventory.Add(item);
                item.Init(_materialsData);
            }
            foreach (var decor in progress.InventoryDecors)
            {
                AllItemsInInventory.Add(decor);
            }
        }
        LoasSave?.Invoke();

        Debug.Log("LoadProgress " + AllItemsInInventory.Count);
    }
}

