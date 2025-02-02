using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private ButtonClickChangeImage[] buttonsClick;

    private StateMachine _stateMachine;
    private InventorySystem _inventory;

    [Inject]
    public void  Construct(StateMachine stateMachine, InventorySystem inventory)
    {
        _stateMachine = stateMachine;
        _inventory = inventory;

        _workbenchPanel.SetActive(false);

        foreach (var button in buttonsClick)
        {
            button.GetComponent<ButtonEnterChangeImage>().Activate();
        }
    }

    public void ActivateInventory()
    {
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
        _workbenchPanel.SetActive(false);
        _stateMachine.Enter<GameLoopState>();
        _inventory.gameObject.SetActive(false);
    }
}
