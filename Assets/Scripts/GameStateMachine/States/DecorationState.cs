using System;

public class DecorationState : IState
{
    private readonly SpaceDeterminantor _spaceDeterminantor;
    private readonly DecorationSystem _decorationSystem;
    private readonly GameFactory _gameFactory;

    public DecorationState(GameFactory gameFactory, DecorationSystem decorationSystem, SpaceDeterminantor spaceDeterminantor)
    {
        _spaceDeterminantor = spaceDeterminantor;
        _decorationSystem = decorationSystem;
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
        //_decorationSystem.SetCanDecorate(true);
        //_gameFactory.HeroMove.Immobilize();
    }

    public void Exit()
    {
       // _decorationSystem.SetCanDecorate(false);
    }//
}

    