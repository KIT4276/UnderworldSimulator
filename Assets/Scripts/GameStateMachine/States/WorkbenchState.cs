using UnityEngine;

public class WorkbenchState : IState
{
    private readonly GameFactory _gameFactory;

    public WorkbenchState(GameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }


    public void Enter()
    {
        _gameFactory.HeroMove.Immobilize();
    }

    public void Exit()
    {
        
    }
}
