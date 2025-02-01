using UnityEngine;
using Zenject;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;

    private DecorFactory _decorFactory;

    [Inject]
    private void Construct(DecorFactory decorFactory)
    {
        _decorFactory = decorFactory;
        _decorFactory.OnSpawned += OnDecorSpawned;
    }

    private void Start()
    {
        DeActivateInventory();
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
        Debug.Log("ReturnToInventory");
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if(!_inventorySlot[i].IsOccupied)
            {
                Debug.Log(i);
                _inventorySlot[i].SetDecor(decor);
                break;
            }
        }
    }
}
