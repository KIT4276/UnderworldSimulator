using System;
using UnityEngine;
using Zenject;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] private ProgressPanel _progressPanel;
    [SerializeField] private ProgressPanel _milestonePanel;
    [SerializeField] private GameObject _bottomPanel;

    private StatesTransitor _transitor;
    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StatesTransitor transitor, ProgressSystem progressSystem, MilestoneSystem milestoneSystem, StateMachine stateMachine)
    {
        _transitor = transitor;
        _stateMachine = stateMachine;

        _progressPanel.Init(progressSystem);
        _milestonePanel.Init(milestoneSystem);
    }

    private void Start()
    {
        _stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if(state is WorkbenchState)
        {
            _bottomPanel.SetActive(false);
        }
        else if(state is GameLoopState)
        {
            _bottomPanel.SetActive(true);
        }
    }

    public void OpenInventory()
    {
        _transitor.ConditionalToInventoryState();
    }

    public void OpenCraft()
    {
        _transitor.ToPseudoCraft();
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
