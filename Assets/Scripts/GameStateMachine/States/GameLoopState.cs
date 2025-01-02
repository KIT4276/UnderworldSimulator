using System;
using UnityEngine;

public class GameLoopState : IState
{
    //private readonly WorkbenchSystem _workbenchSystem;
    private readonly GameFactory _gameFactory;

    public GameLoopState(/*WorkbenchSystem workbenchSystem,*/ GameFactory gameFactory)
    {
        //_workbenchSystem = workbenchSystem;
        _gameFactory = gameFactory;
    }

    public void Enter() 
    {
        _gameFactory.HeroMove.Mobilize();
        _gameFactory.CameraMove.Immobilize();
        
    }

    public void Exit() 
    {
        _gameFactory.HeroMove.Immobilize();
        _gameFactory.CameraMove.Mobilize();
    }
}