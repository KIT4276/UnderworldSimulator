using System.Collections;
using UnityEngine;
using Zenject;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;
    [SerializeField] private GameObject _warningSign;

    private DecorHolder _decorHolder;
    private DecorationSystem _decorationSystem;

    [Inject]
    public void Construct( DecorationSystem decorationSystem, DecorHolder decorHolder)
    {
        _decorHolder = decorHolder;
        _decorationSystem = decorationSystem;
        _decorationSystem.TryToRemoveDecorAction += TryReturnToInventory;
        _warningSign.SetActive(false);
    }

    public void ActivateInventory()
    {
        foreach (var slot in _inventorySlot)
        {
            slot.Initialized();
        }
    }

    public void DeActivateInventory()
    {
        if (_decorHolder.ActiveDecor == null)
            this.gameObject.SetActive(false);
    }

    private void TryReturnToInventory(Decor decor)
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i].IsOccupied)
            {
                if (_inventorySlot[i].GetLastDecor().DecorType == decor.DecorType)
                {
                    ReturnToInventory(decor, i);
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
                    ReturnToInventory(decor, i);
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

    private void ReturnToInventory(Decor decor, int i)
    {
        _inventorySlot[i].SetDecor(decor);
        _decorationSystem.ReturtDecorToInventory(decor);
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
        _decorationSystem.TryToRemoveDecorAction -= TryReturnToInventory;
    }
}
