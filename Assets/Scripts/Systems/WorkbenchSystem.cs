using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private ButtonClickChangeImage[] buttonsClick;
    [SerializeField] private GameObject _warningSign;
    [SerializeField] private RoomMenu _roomRating;
    [SerializeField] private GuestMenu _guestMenu;


    private DecorHolder _decorHolder;
    private StateMachine _stateMachine;
    private InventorySystem _inventory;
    private DecorationSystem _decorationSystem;

    public event Action InventoryButtonClick;
    public event Action CraftButtonClick;
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
        _roomRating.gameObject.SetActive(false);
        _guestMenu.gameObject.SetActive(false);

        foreach (var button in buttonsClick)
        {
            button.GetComponent<ButtonEnterChangeImage>().Activate();
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
        Debug.Log("OnExitWorkbench");
        _roomRating.gameObject.SetActive(false);
        Exit?.Invoke();
    }

    public void OnInventoryButtonClick()
    {
        InventoryButtonClick?.Invoke();
    }

    public void OnCraftButtonClick()
    {
        CraftButtonClick?.Invoke();
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
        _roomRating.gameObject.SetActive(true);

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
        Destroyed?.Invoke();
    }
}
