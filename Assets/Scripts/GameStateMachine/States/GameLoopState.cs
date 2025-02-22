using System;

public class GameLoopState : IState
{
    public event Action GameLoopStateEnter;

    public GameLoopState()
    {
    }

    public void Enter() 
    {
        GameLoopStateEnter?.Invoke();
    }

    public void Exit() 
    {
    }
}