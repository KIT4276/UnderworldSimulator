using System;
using UnityEngine;

public class GameLoopState : IState
{
    private readonly LoadingCurtain _loadingCurtain;
    private readonly StateMachine _stateMachine;

    public event Action GameLoopStateEnter;

    public GameLoopState(LoadingCurtain loadingCurtain, StateMachine stateMachine)
    {
        _loadingCurtain = loadingCurtain;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        //Debug.Log("Enter GameLoopState");
        if (_stateMachine.IsTests && _stateMachine.PredioslyState is LoadLevelState)
            _loadingCurtain.Hide();

        GameLoopStateEnter?.Invoke();
    }

    public void Exit()
    {
    }
}