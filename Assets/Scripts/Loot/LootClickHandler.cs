using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LootClickHandler : MonoBehaviour
{
    [SerializeField] private LootSlot _lootSlot;
    [SerializeField] private InputActionReference _E_pressedAction;
    private CraftLoot _craftLoot;


    [Inject] private LootSystem _lootSystem;
    [Inject] private StateMachine _stateMachine;

    private void Start()
    {
        _E_pressedAction.action.started += OnEPressed;
    }

    private void OnEPressed(InputAction.CallbackContext context)
    {
        Debug.Log("OnEPressed");
        if (_stateMachine.ActiveState is LootState)
        {
            OnTakeAllClick();
        }
    }

    public void OntakeClick()
    {
        Debug.Log("OntakeClick");
        IBaseItem takenLoot = _lootSlot.TakeLastItem();
        _lootSystem.TakeLootToInventory(takenLoot);

        _craftLoot.TakeItem(((Item)takenLoot).LootType, 1);


        if (_lootSlot.Loots.Count <= 0)
        {
            // Debug.Log(_lootSlot.Loots.Count);
            _lootSystem.AllIsTacen();
        }

        AudioManager.Instance.Play(SoundEnum.Inventory_Add);
    }

    public void OnTakeAllClick()
    {

        Debug.Log("OnTakeAllClick");
        while (_lootSlot.Loots.Count > 0)
        {
            _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
        }
        _lootSystem.AllIsTacen();

        if (!AudioManager.Instance.IsPlaying(SoundEnum.Inventory_Add)) AudioManager.Instance.Play(SoundEnum.Inventory_Add);
    }

    private void OnDestroy()
    {
        _E_pressedAction.action.performed -= OnEPressed;
    }

    public void AddCraftLoot(CraftLoot craftLoot)
    {
        _craftLoot = craftLoot;
    }
}
