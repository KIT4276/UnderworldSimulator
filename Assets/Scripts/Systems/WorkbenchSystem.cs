using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private ButtonClickChangeImage[] buttonsClick;
    [SerializeField] private InputActionReference _escapeAction;


    private DecorHolder _decorHolder;
    private StateMachine _stateMachine;
    private InventorySystem _inventory;
    private DecorationSystem _decorationSystem;

    [Inject]
    public void Construct(StateMachine stateMachine, InventorySystem inventory, DecorationSystem decorationSystem, DecorHolder decorHolder)
    {
        _decorHolder = decorHolder;
        _stateMachine = stateMachine;
        _inventory = inventory;
        _decorationSystem = decorationSystem;
        _inventory.gameObject.SetActive(false);
        _workbenchPanel.SetActive(false);

        foreach (var button in buttonsClick)
        {
            button.GetComponent<ButtonEnterChangeImage>().Activate();
        }
        _stateMachine.ChangeStateAction += OnChangeState;
        _escapeAction.action.performed += OnEscape;
    }

    public void OnInventoryButtonClick()
    {
        _stateMachine.Enter<DecorationState>();
    }

    private void OnChangeState(IExitableState state)
    {
       if(state is WorkbenchState )
        {
            DeActivateInventory();
            ActivateWorkbench();
        }
       else if(state is DecorationState ) 
        {
            // todo highlight Inventory button
            ActivateInventory();
        }
       else if(state is GameLoopState)
        {
            DeActivateInventory();
            DeActivateWorkbench();
        }
       
    }

    private void ActivateInventory()
    {
        _inventory.gameObject.SetActive(true);
        _inventory.ActivateInventory();
        //_stateMachine.Enter<DecorationState>(); // trmporary
    }

    private void DeActivateInventory()
    {
        _inventory.gameObject.SetActive(false);
    }

    private void ActivateWorkbench()
    {
        _workbenchPanel.SetActive(true);

        foreach (var button in buttonsClick)
        {
            button.RestartView();
        }

        //_stateMachine.Enter<WorkbenchState>();
    }

    private void DeActivateWorkbench()
    {
        if (_decorHolder.ActiveDecor == null)
        {
            _workbenchPanel.SetActive(false);
            //_stateMachine.Enter<GameLoopState>();
            _inventory.gameObject.SetActive(false);

        }
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        //if (_stateMachine.ActiveState is WorkbenchState)
        //    DeActivateWorkbench();

        if(_stateMachine.ActiveState is DecorationState)
        {
            _stateMachine.Enter<WorkbenchState>();
        }
        else if(_stateMachine.ActiveState is WorkbenchState)
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        _escapeAction.action.performed -= OnEscape;
    }
}
