using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;
    [SerializeField] private GameObject _warningSign;

    private DecorHolder _decorHolder;
    private DecorationSystem _decorationSystem;

    public event Action Closed;

    [Inject]
    public void Construct( DecorationSystem decorationSystem, DecorHolder decorHolder)
    {
        _decorHolder = decorHolder;
        _decorationSystem = decorationSystem;
        _decorationSystem.TryToRemoveDecorAction += TryReturnDecorToInventory;
        _warningSign.SetActive(false);
    }

    public void ActivateInventory()
    {
        foreach (var slot in _inventorySlot)
        {
            slot.Initialize();
        }
    }

    public void DeActivateInventory()
    {
        if (_decorHolder.ActiveDecor == null)
        {
            this.gameObject.SetActive(false);
            Closed?.Invoke();
        }
    }

    public void TryReturnLootToInventory(Loot loot) /// �����������! ���� ���������� ������ ����� ������� ���
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i].IsOccupied)
            {
                if (_inventorySlot[i].GetLastItems() is Loot && 
                    ((Loot)_inventorySlot[i].GetLastItems()).LootType == loot.LootType)
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
            Debug.Log(" �� ������� ����� ��� ������");
            StopAllCoroutines();
            _warningSign.SetActive(true);
            StartCoroutine(HideTAblet());
        }
    }

    private void TryReturnDecorToInventory(Decor decor)//�����������! ���� ����������, ������ ���� ����� ������� �����.
                                                       //��� ���� ������� ���� �����
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
            Debug.Log(" �� ������� ����� ��� ������");
            StopAllCoroutines();
            _warningSign.SetActive(true);
            StartCoroutine(HideTAblet());
        }
    }

    private void ReturnDecorToInventory(Decor decor, int i)//�����������! ���� ����������, ������ ���� ����� ������� �����.
                                                           //��� ���� ������� ���� �����
    {
        _inventorySlot[i].SetItem(decor);
        _decorationSystem.ReturtDecorToInventory(decor);
    }

    private void ReturnLootToInventory(Loot loot, int i)//�����������! ���� ����������, ������ ���� ����� ������� ���.
    {
        _inventorySlot[i].SetItem(loot);
        //_decorationSystem.ReturtDecorToInventory(loot);
    }

    private IEnumerator HideTAblet()
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
