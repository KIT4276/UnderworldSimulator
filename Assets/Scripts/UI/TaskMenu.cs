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
        stateMachine.ChangeStateAction += OnChangeState;
        _stateMachine = stateMachine;

       
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
            Debug.Log(_tasksHandler.AvailableList.Count);

            if (_tasksHandler.AvailableList.Count > i)
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
    }
}
