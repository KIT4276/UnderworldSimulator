using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    //[SerializeField] private GameObject _decorationPanel;

    private StateMachine _stateMachine;
    private InventorySystem _inventory;

    [Inject]
    public void  Construct(StateMachine stateMachine, InventorySystem inventory)
    {
        _stateMachine = stateMachine;
        _inventory = inventory;

        _workbenchPanel.SetActive(false);
        //_decorationPanel.SetActive(false);
    }

    public void ActivateInventory()
    {
        _inventory.gameObject.SetActive(true);
    }

    public void ActivateWorkbench()
    {
        _workbenchPanel.SetActive(true );
        //_stateMachine.Enter<WorkbenchState>();

        _stateMachine.Enter<DecorationState>(); // trmporary
    }

    public void DeActivateWorkbench()
    {
        _workbenchPanel.SetActive(false);
        _stateMachine.Enter<GameLoopState>();
        _inventory.gameObject.SetActive(false);
    }

    //public void ActivateDecoration() 
    //{
    //    //SwitchPanels(false);
    //    _stateMachine.Enter<DecorationState>();
    //}

    //public void DeActivateDecoration()
    //{
    //    _stateMachine.Enter<WorkbenchState>();
    //    //SwitchPanels(true);
    //}

    //private void SwitchPanels(bool isWorkbench)
    //{
    //    _workbenchPanel.SetActive(isWorkbench);
    //    _decorationPanel.SetActive(!isWorkbench);
    //}
}
