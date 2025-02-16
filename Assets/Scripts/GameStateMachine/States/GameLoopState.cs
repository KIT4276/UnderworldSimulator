using System;

public class GameLoopState : IState
{
    private readonly GameFactory _gameFactory;

    public event Action EnterGameLoopState;

    public GameLoopState( GameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void Enter() 
    {
        _gameFactory.HeroMove.Mobilize();
        _gameFactory.CameraMove.Immobilize();
        EnterGameLoopState?.Invoke();
    }

    public void Exit() 
    {
        //_gameFactory.HeroMove.Immobilize();
        //_gameFactory.CameraMove.Mobilize();
    }
}