using System;
using UnityEngine;

public class WorkbenchState : IState
{
    public event Action WorkbenchStateEnter;


    private readonly GameFactory _gameFactory;
    private readonly DecorationSystem _decorationSystem;
    private readonly SpaceDeterminantor _spaceDeterminantor;

    public WorkbenchState(GameFactory gameFactory, DecorationSystem decorationSystem, SpaceDeterminantor spaceDeterminantor)
    {
        _gameFactory = gameFactory;
        _decorationSystem = decorationSystem;
        _spaceDeterminantor = spaceDeterminantor;
    }


    public void Enter()
    {
        //_decorationSystem.SetCanDecorate(true);
        //_spaceDeterminantor.FindDecorableSpace();

        //_gameFactory.HeroMove.Immobilize();
        //_gameFactory.CameraMove.Mobilize();
        //WorkbenchStateEnter?.Invoke();
    }

    public void Exit()
    {
        //_decorationSystem.SetCanDecorate(false);
    }
}
