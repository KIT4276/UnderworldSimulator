using System;
using UnityEngine;
using Zenject;

public class TaskMenu : MonoBehaviour
{
    [SerializeField] private ParameterSlot[] _parametersSlots;

    private TasksHandler _tasksHandler;
    private bool _isInit;
    private StateMachine _stateMachine;

    [Inject]
    private void  Construct(TasksHandler tasksHandler, StateMachine stateMachine)
    {
        _tasksHandler = tasksHandler;
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        _stateMachine.ChangeStateAction += OnChangeState;
        _tasksHandler.AvailableUpdate += OnAvailableUpdate;

        foreach(var task in _tasksHandler.All)
        {
            task.BecameAvailable += OnBecameAvailable;
        }
        
       
    }

    private void Awake()
    {
        Fill();
    }

    private void OnBecameAvailable(BaseHandledReward reward)
    {
        Fill();
    }

    private void OnAvailableUpdate()
    {
        Fill();
    }

    private void OnChangeState(IExitableState state)
    {
        if (!_isInit && state is GameLoopState)
        {
            Fill();
            _isInit = true;
        }
    }

    private void Fill()
    {
        for (int i = 0; i < _parametersSlots.Length; i++)
        {

            if (_tasksHandler.AvailableList.Count >i)
            {
                _parametersSlots[i].FillSlot((Task)_tasksHandler.AvailableList[i]);
            }
            else
            {
                _parametersSlots[i].FillEmpty();
            }
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        _tasksHandler.AvailableUpdate -= OnAvailableUpdate;
        _tasksHandler.OnDestroy();
    }
}
