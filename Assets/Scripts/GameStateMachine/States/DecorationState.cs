using System;

public class DecorationState : IState
{
    private readonly SpaceDeterminantor _spaceDeterminantor;
    private readonly DecorationSystem _decorationSystem;

    public event Action DecorationStateEnter;
    public event Action DecorationStateExit;

    public DecorationState(GameFactory gameFactory, DecorationSystem decorationSystem, SpaceDeterminantor spaceDeterminantor)
    {
        _spaceDeterminantor = spaceDeterminantor;
        _decorationSystem = decorationSystem;
    }

    public void Enter()
    {
        DecorationStateEnter?.Invoke();
        _decorationSystem.SetIsOnDecorState(true);
        _spaceDeterminantor.FindDecorableSpace();
    }

    public void Exit()
    {
        _decorationSystem.SetIsOnDecorState(false);
        DecorationStateExit?.Invoke();
    }
}