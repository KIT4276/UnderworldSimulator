using UnityEngine;

public class InventoryFilters : MonoBehaviour
{
    [SerializeField] private FilterButtonSwitch _filterButtonSwitch;
    [Space]
    [SerializeField] private InventorySystem _inventorySystem;


    private void Start()
    {
        _inventorySystem.ActivateInventoryEvent += OnActivateInventory;
        _filterButtonSwitch.Switch(FilterType.NoFilter);
    }


    public void OnNoFilter()
    {
        _inventorySystem.ClearSlots();
        _inventorySystem.FillDecorItems();
        _inventorySystem.FillCraftItems();

        _filterButtonSwitch.Switch(FilterType.NoFilter);
    }

    public void OnDecorFilter()
    {
        _inventorySystem.ClearSlots();
        _inventorySystem.FillDecorItems();
        _filterButtonSwitch.Switch(FilterType.Decor);
    }

    public void OnCraftFilter()
    {
        _inventorySystem.ClearSlots();
        _inventorySystem.FillCraftItems();
        _filterButtonSwitch.Switch(FilterType.Craft);
    }


    private void OnActivateInventory()
    {
        OnNoFilter();
    }


    private void OnDestroy()
    {
        _inventorySystem.ActivateInventoryEvent -= OnActivateInventory;
    }
}
