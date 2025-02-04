using UnityEngine;
using Zenject;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;

    private DecorFactory _decorFactory;
    private DecorationSystem _decorationSystem;

    [Inject]
    private void Construct(DecorFactory decorFactory, DecorationSystem decorationSystem)
    {
        _decorFactory = decorFactory;
        _decorationSystem = decorationSystem;
        _decorFactory.OnSpawned += OnDecorSpawned;
    }

    public void OnDecorSpawned(Decor decor)
    {
        decor.CanceledAction += ReturnToInventory;
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
        this.gameObject.SetActive(false);
    }

    public void ReturnToInventory(Decor decor)
    {
        Debug.Log(decor);
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (!_inventorySlot[i].IsOccupied)
            {
                _inventorySlot[i].SetDecor(_decorationSystem.ActiveDecor);
                break;
            }
        }
    }
}
