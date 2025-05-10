using UnityEngine;

public class InventoryFilters : MonoBehaviour
{
    [SerializeField] private FilterButtonSwitch _filterButtonSwitch;
    [Space]
    [SerializeField] private InventorySystem _inventorySystem;

    private FilterType _filterType;


    private void Start()
    {
        _inventorySystem.ActivateInventoryEvent += OnActivateInventory;
    }

    private void Awake()
    {
        _filterButtonSwitch.Switch(FilterType.NoFilter);
        _filterType = FilterType.NoFilter;
    }

    public void UpdateFiltres()
    {
        switch (_filterType)
        {
            case FilterType.NoFilter:
                OnNoFilter();
                break;
            case FilterType.Decor:
                OnDecorFilter();
                break;
            case FilterType.Craft:
                OnCraftFilter();
                break;
            default:
                OnNoFilter();
                break;
        }
    }


    public void OnNoFilter()
    {
        //_inventorySystem.ClearSlots();
        //_inventorySystem.FillDecorItems();
        //_inventorySystem.FillCraftItems();

        _inventorySystem.OffAllOccupiedSlots();

        _inventorySystem.OnDecorSlots();
        _inventorySystem.OnCraftSlots();

        _filterButtonSwitch.Switch(FilterType.NoFilter);
        _filterType = FilterType.NoFilter;
    }

    public void OnDecorFilter()
    {
        //_inventorySystem.ClearSlots();
        //_inventorySystem.FillDecorItems();

        _inventorySystem.OffAllOccupiedSlots();
        _inventorySystem.OnDecorSlots();

        _filterButtonSwitch.Switch(FilterType.Decor);
        _filterType = FilterType.Decor;
    }

    public void OnCraftFilter()
    {
        //_inventorySystem.ClearSlots();
        //_inventorySystem.FillCraftItems();

        _inventorySystem.OffAllOccupiedSlots();
        _inventorySystem.OnCraftSlots();

        _filterButtonSwitch.Switch(FilterType.Craft);
        _filterType = FilterType.Craft;
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
