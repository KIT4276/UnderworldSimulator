using UnityEngine;

public class DecorationState : IState
{
    private readonly DecorationSystem _decorationSystem;

    public DecorationState(GameFactory gameFactory, DecorationSystem decorationSystem)
    {
        _decorationSystem = decorationSystem;
    }

    public void Enter()
    {
        _decorationSystem.SetIsOnDecorState(true);
    }

    public void Exit()
    {
        _decorationSystem.SetIsOnDecorState(false);
    }
}