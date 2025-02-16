using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LootSystem : MonoBehaviour
{
    [SerializeField] private InputActionReference _escapeAction;
    [SerializeField] private GameObject _menu;
    [SerializeField] private LootSlot[] _slots;
    
    [Inject] private InventorySystem _inventorySystem;

    private bool _isInited;
    private GameObject _interactiveObject;

    public event Action OpenMenuAction;

    void Start()
    {
        _menu.SetActive(false);
        _inventorySystem.Closed += CloseMenu;
        _escapeAction.action.performed += OnEscape;
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
        OpenMenuAction?.Invoke();
    }

    public void CloseMenu()
    {
        _menu.SetActive(false);
    }

    public void TakeLootToInventory(BaseItem item)
    {
        _inventorySystem.TryReturnLootToInventory((Loot)item);
    }

    public void OffInteractiveObject()
    {
        _interactiveObject.SetActive(false);
    }

    public void FillSlot(Loot loot, int count, GameObject interactiveObject)
    {
        foreach(var slot in _slots)
        {
            if(!slot.IsOccupied)
            {
                for (int i = 0; i < count; i++)
                {
                    slot.SetItem(loot);
                }
                _interactiveObject = interactiveObject;
                break;
            }
        }
    }


    private void OnEscape(InputAction.CallbackContext context)
    {
        Debug.Log("OnEscape");
        CloseMenu();
    }

    private void OnDestroy()
    {
        _inventorySystem.Closed -= CloseMenu;
    }
}
