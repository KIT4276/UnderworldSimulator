using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LootClickHandler : MonoBehaviour
{
    [SerializeField] private LootSlot _lootSlot;
    [SerializeField] private InputActionReference _E_pressedAction;


    [Inject] private LootSystem _lootSystem;
    [Inject] private StateMachine _stateMachine;

    private void Start()
    {
        _E_pressedAction.action.started += OnEPressed;
    }

    private void OnEPressed(InputAction.CallbackContext context)
    {
        if (_stateMachine.ActiveState is LootState)
        {
            OnTakeAllClick();
        }
    }

    public void OntakeClick()
    {
        //_lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());

        //if (_lootSlot.Loots.Count <= 0)
        //{
        //   // Debug.Log(_lootSlot.Loots.Count);
        //    _lootSystem.AllIsTacen();
        //}
    }

    public void OnTakeAllClick()
    {
        while (_lootSlot.Loots.Count > 0)
        {
            _lootSystem.TakeLootToInventory(_lootSlot.TakeLastItem());
        }
        _lootSystem.AllIsTacen();
    }

    private void OnDestroy()
    {
        _E_pressedAction.action.performed -= OnEPressed;
    }
}
