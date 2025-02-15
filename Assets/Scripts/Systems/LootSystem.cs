using System;
using UnityEngine;
using Zenject;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private LootSlot[] _slots;
    
    [Inject] private InventorySystem _inventorySystem;

    private bool _isInited;
    
    void Start()
    {
        _menu.SetActive(false);
        _inventorySystem.Closed += CloseMenu;
    }

    public void OpenMenu()
    {
        _menu.SetActive(true);
        _inventorySystem.gameObject.SetActive(true);
        _inventorySystem.ActivateInventory();

        if (!_isInited)
        {
            foreach (var slot in _slots)
            {
                slot.Initialize();
            }
            _isInited = true;
        }
    }

    public void CloseMenu()
    {
        _menu.SetActive(false);
    }

    public void TakeLootToInventory(BaseItem item)
    {
        _inventorySystem.TryReturnLootToInventory((Loot)item);
    }

    private void OnDestroy()
    {
        _inventorySystem.Closed -= CloseMenu;
    }
}
