using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private ButtonClickChangeImage[] _buttonsClick;
    [SerializeField] private GameObject _warningSign;
    [SerializeField] private float _delay = 1;
    [SerializeField] private ButtonEnterChangeImage[] _arrows;

    private DecorHolder _decorHolder;
    private StateMachine _stateMachine;
    private InventorySystem _inventory;

    public event Action InventoryButtonClick;
    public event Action CraftButtonClick;
    public event Action Exit;
    public event Action Destroyed;

    [Inject]
    public void Construct(StateMachine stateMachine, InventorySystem inventory, DecorHolder decorHolder)
    {
        _decorHolder = decorHolder;
        _stateMachine = stateMachine;
        _inventory = inventory;
        _inventory.gameObject.SetActive(false);
        _workbenchPanel.SetActive(false);
        _warningSign.SetActive(false);

        foreach (var button in _buttonsClick)
        {
            button.Init(_delay);
            button.GetComponent<SlotEnterChangeImage>().Activate();
        }

        _stateMachine.ChangeStateAction += OnChangeState;
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
       
        StartCoroutine(ButtonClickRoutine(Exit));
    }

    public void OnInventoryButtonClick()
    {
        StartCoroutine(ButtonClickRoutine(InventoryButtonClick));
    }

    public void OnCraftButtonClick()
    {
        StartCoroutine(ButtonClickRoutine(CraftButtonClick));
    }

    private IEnumerator ButtonClickRoutine(Action action)
    {
        yield return new WaitForSeconds(_delay);
        action?.Invoke();
    }

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

        foreach (var button in _buttonsClick)
        {
            button.RestartView();
        }
        foreach (var arrow in _arrows)
        {
            arrow.Activate();
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
