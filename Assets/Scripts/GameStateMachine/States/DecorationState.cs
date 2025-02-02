using System;

public class DecorationState : IState
{
    private readonly DecorationSystem _decorationSystem;

    public event Action DecorationStateEnter;
    public event Action DecorationStateExit;

    public DecorationState(GameFactory gameFactory, DecorationSystem decorationSystem)
    {
        _decorationSystem = decorationSystem;
    }

    public void Enter()
    {
        _decorationSystem.SetIsOnDecorState(true);
        DecorationStateEnter?.Invoke();
    }

    public void Exit()
    {
        _decorationSystem.SetIsOnDecorState(false);
        DecorationStateExit?.Invoke();
    }
}