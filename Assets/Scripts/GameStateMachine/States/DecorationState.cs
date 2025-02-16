using System;

public class DecorationState : IState
{
    private readonly SpaceDeterminantor _spaceDeterminantor;
    private readonly DecorationSystem _decorationSystem;
    private readonly GameFactory _gameFactory;

    public event Action DecorationStateEnter;
    public event Action DecorationStateExit;

    public DecorationState(GameFactory gameFactory, DecorationSystem decorationSystem, SpaceDeterminantor spaceDeterminantor)
    {
        _spaceDeterminantor = spaceDeterminantor;
        _decorationSystem = decorationSystem;
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
        DecorationStateEnter?.Invoke();
        _decorationSystem.SetIsOnDecorState(true);
        _spaceDeterminantor.FindDecorableSpace();

        _gameFactory.HeroMove.Immobilize();
        _gameFactory.CameraMove.Mobilize();
    }

    public void Exit()
    {
        _decorationSystem.SetIsOnDecorState(false);
        DecorationStateExit?.Invoke();
    }
}

    