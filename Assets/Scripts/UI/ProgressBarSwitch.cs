using System;
using UnityEngine;
using Zenject;

public class ProgressBarSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _progressBar;

    private StateMachine _stateMachine;
    private MilestoneSystem _milestoneSystem;
    private MilestonesData _milestonesData;

    private bool _disposed = false;

    [Inject]
    private void Construct(StateMachine stateMachine, MilestoneSystem milestoneSystem, MilestonesData milestonesData)
    {
        _stateMachine = stateMachine;
        _milestoneSystem = milestoneSystem;
        _milestonesData = milestonesData;
        _stateMachine.ChangeStateAction += OnChangeState;
        _milestoneSystem.TheEnd += OnTheEnd;
    }

    private void OnTheEnd()
    {
            Debug.Log("last Milestone");
        _disposed = true;
            //_progressBar.SetActive(false);
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is DecorationState || state is WorkbenchState || state is CraftState)
        {
            if (!_disposed)
            {
                _progressBar.SetActive(true);
            }
        }
        else
        {
            _progressBar.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
