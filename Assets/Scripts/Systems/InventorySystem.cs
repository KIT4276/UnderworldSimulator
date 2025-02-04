using System.Collections;
using UnityEngine;
using Zenject;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;
    [SerializeField] private GameObject _warningSign;

    private DecorFactory _decorFactory;
    private DecorationSystem _decorationSystem;

    [Inject]
    private void Construct(DecorFactory decorFactory, DecorationSystem decorationSystem)
    {
        _decorFactory = decorFactory;
        _decorationSystem = decorationSystem;
        //_decorFactory.OnSpawned += OnDecorSpawned;
        _decorationSystem.TryToRemoveDecorAction += TryReturnToInventory;
        //_decorationSystem.RemoveDecorAction += ReturnToInventory;
        _warningSign.SetActive(false);
    }

    //public void OnDecorSpawned(Decor decor)
    //{
    //    decor.CanceledAction += ReturnToInventory;
    //}

    public void ActivateInventory()
    {
        foreach (var slot in _inventorySlot)
        {
            slot.Initialized();
        }
    }

    public void DeActivateInventory()
    {
        if(_decorationSystem.ActiveDecor == null)
        this.gameObject.SetActive(false);
    }

    private void TryReturnToInventory(Decor decor)
    {
        bool isPlaced = false;

        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (!_inventorySlot[i].IsOccupied)
            {
                _inventorySlot[i].SetDecor(_decorationSystem.ActiveDecor);
                _decorationSystem.ReturtDecorToInventory();
                isPlaced = true;
                break;
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

    private IEnumerator HideTAblet()
    {
        yield return new WaitForSeconds(3);
        _warningSign.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _warningSign.SetActive(false);
    }
}
