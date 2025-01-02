using System;
using UnityEngine;

public class GameLoopState : IPayloadedState<HeroMove>
{
    private  HeroMove _heroMove;
    private readonly WorkbenchSystem _workbenchSystem;

    public GameLoopState(WorkbenchSystem workbenchSystem)
    {
        _workbenchSystem = workbenchSystem;
    }

    public void Enter(HeroMove heroMove) 
    {
        _heroMove = heroMove;
        _heroMove.Mobilize();
        _workbenchSystem.Activate();
    }

    public void Exit() 
    {
        _heroMove.Immobilize(); 
    }
}