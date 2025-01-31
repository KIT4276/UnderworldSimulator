using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlot;

    private void Start()
    {
        DeActivateInventory();
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
}
