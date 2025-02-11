using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private ButtonClickChangeImage[] buttonsClick;
    private DecorHolder _decorHolder;
    private StateMachine _stateMachine;
    private InventorySystem _inventory;
    private DecorationSystem _decorationSystem;

    [Inject]
    public void  Construct(StateMachine stateMachine, InventorySystem inventory, DecorationSystem decorationSystem, DecorHolder decorHolder)
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
    }

    public void ActivateInventory()
    {
        Debug.Log("ActivateInventory"); 
        _inventory.gameObject.SetActive(true);
        _inventory.ActivateInventory();
        _stateMachine.Enter<DecorationState>(); // trmporary
    }

    public void ActivateWorkbench()
    {
        _workbenchPanel.SetActive(true );

        foreach (var button in buttonsClick)
        {
            button.RestartView();
        }

        _stateMachine.Enter<WorkbenchState>();
    }

    public void DeActivateWorkbench()
    {
        if (_decorHolder.ActiveDecor == null)
        {

            _workbenchPanel.SetActive(false);
            _stateMachine.Enter<GameLoopState>();
            _inventory.gameObject.SetActive(false);
        }
    }
}
