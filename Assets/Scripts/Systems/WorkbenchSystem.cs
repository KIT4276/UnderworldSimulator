using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _workbenchPanel;
    [SerializeField] private GameObject _decorationPanel;
    [SerializeField] private GameObject _workbenchButton;

    private StateMachine _stateMachine;
    private GameFactory _gameFactory;

    [Inject]
    public void  Construct(StateMachine stateMachine, GameFactory gameFactory)
    {
        _stateMachine = stateMachine;
        _gameFactory = gameFactory;

        _workbenchPanel.SetActive(false);
        _decorationPanel.SetActive(false);
        _workbenchButton.SetActive(false);
    }

    public void ActivateWorkbench()
    {
        SwitchPanels(true);
    }

    public void DeActivateWorkbench()
    {
        _workbenchPanel.SetActive(false);
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
        _stateMachine.Enter<GameLoopState, HeroMove>(_gameFactory.HeroMove);
    }

    public void Activate()
    {
        _workbenchButton.SetActive(true);
    }

    private void SwitchPanels(bool isWorkbench)
    {
        _workbenchPanel.SetActive(isWorkbench);
        _decorationPanel.SetActive(!isWorkbench);
    }
}
