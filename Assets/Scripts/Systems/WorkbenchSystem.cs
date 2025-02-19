using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private ButtonClickChangeImage[] buttonsClick;
    // [SerializeField] private InputActionReference _escapeAction;
    [SerializeField] private GameObject _warningSign;


    private DecorHolder _decorHolder;
    private StateMachine _stateMachine;
    private InventorySystem _inventory;
    private DecorationSystem _decorationSystem;

    public event Action InventoryButtonClick;
    public event Action Exit;
    public event Action Destroyed;

    [Inject]
    public void Construct(StateMachine stateMachine, InventorySystem inventory, DecorationSystem decorationSystem, DecorHolder decorHolder)
    {
        _decorHolder = decorHolder;
        _stateMachine = stateMachine;
        _inventory = inventory;
        _decorationSystem = decorationSystem;
        _inventory.gameObject.SetActive(false);
        _workbenchPanel.SetActive(false);
        _warningSign.SetActive(false);

        foreach (var button in buttonsClick)
        {
            button.GetComponent<ButtonEnterChangeImage>().Activate();
        }

        _stateMachine.ChangeStateAction += OnChangeState;
        //_escapeAction.action.performed += OnEscape;
    }

    public void ShowSign()
    {
        StopAllCoroutines();
        _warningSign.SetActive(true);
        StartCoroutine(HideSign());
    }

    private IEnumerator HideSign()
    {
        yield return new WaitForSeconds(3);
        _warningSign.SetActive(false);
    }

    public void OnExitWorkbench()
    {
        Exit?.Invoke();
    }

    public void OnInventoryButtonClick()
    {
        InventoryButtonClick?.Invoke(); 
    }

    //private void OnEscape(InputAction.CallbackContext context)
    //{
    //    if (_stateMachine.ActiveState is DecorationState)
    //    {
    //        _stateMachine.Enter<WorkbenchState>();
    //    }
    //    else if (_stateMachine.ActiveState is WorkbenchState)
    //    {
    //        OnExitWorkbench();
    //    }
    //}

    private void OnChangeState(IExitableState state)
    {
        switch (state)
        {
            case GameLoopState:
                DeActivateInventory();
                DeActivateWorkbench();
                break;

            case DecorationState:
                // todo highlight Inventory button
                ActivateInventory();
                break;
            case WorkbenchState:
                DeActivateInventory();
                ActivateWorkbench();
                break;
        }
        //if (state is WorkbenchState)
        //{
        //    DeActivateInventory();
        //    ActivateWorkbench();
        //}
        //else if (state is DecorationState)
        //{
        //    // todo highlight Inventory button
        //    ActivateInventory();
        //}
        //else if (state is GameLoopState)
        //{
        //    DeActivateInventory();
        //    DeActivateWorkbench();
        //}
    }

    private void ActivateInventory()
    {
        _inventory.gameObject.SetActive(true);
        _inventory.ActivateInventory();
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
    }

    private void DeActivateWorkbench()
    {
        if (_decorHolder.ActiveDecor == null)
        {
            _workbenchPanel.SetActive(false);
            _inventory.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        //_escapeAction.action.performed -= OnEscape;
        Destroyed?.Invoke();
    }
}
