using UnityEngine;

public class WorkbenchState : IState
{
    private GameFactory _gameFactory;

    public WorkbenchState(GameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
       
    }

    public void Exit()
    {
        _gameFactory.CameraMove.ReturnDefaultPosition();
    }
}