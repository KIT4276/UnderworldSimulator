using System.Collections;
using UnityEngine;
using Zenject;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] private DecorInventorySlot[] _inventorySlot;
    [SerializeField] private GameObject _warningSign;

    private DecorHolder _decorHolder;
    private DecorationSystem _decorationSystem;

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
            this.gameObject.SetActive(false);
    }

    private void TryReturnDecorToInventory(Decor decor)//внимательно! сюда обращаемся, ТОЛЬКО если нужно вернуть декор.
                                                       //для лута создать свой метод
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i].IsOccupied)
            {
                if (((Decor)_inventorySlot[i].GetLastInventoryObject()).DecorType == decor.DecorType)
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
            Debug.Log(" не нашлось место для декора");
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
        _decorationSystem.TryToRemoveDecorAction -= TryReturnDecorToInventory;
    }
}
