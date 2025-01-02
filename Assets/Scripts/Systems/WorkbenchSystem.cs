using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private GameObject _decorationPanel;

    private StateMachine _stateMachine;

    [Inject]
    public void  Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _workbenchPanel.SetActive(false);
        _decorationPanel.SetActive(false);
    }

    public void ActivateWorkbench()
    {
        SwitchPanels(true);
        _stateMachine.Enter<WorkbenchState>();
    }

    public void DeActivateWorkbench()
    {
        _workbenchPanel.SetActive(false);
        _stateMachine.Enter<GameLoopState>();
    }

    /// <summary>
    /// Should be called when activating a workbench
    /// </summary>
    public void ActivateDecoration() 
    {
        SwitchPanels(false);
        _stateMachine.Enter<DecorationState>();
        
    }

    /// <summary>
    ///  /// <summary>
    /// Should be called when deactivating a workbench
    /// </summary>
    public void DeActivateDecoration()
    {
        SwitchPanels(true);
        _stateMachine.Enter<WorkbenchState>();
    }

    private void SwitchPanels(bool isWorkbench)
    {
        _workbenchPanel.SetActive(isWorkbench);
        _decorationPanel.SetActive(!isWorkbench);
    }
}
