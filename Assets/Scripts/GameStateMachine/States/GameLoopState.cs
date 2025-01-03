public class GameLoopState : IState
{
    private readonly GameFactory _gameFactory;

    public GameLoopState( GameFactory gameFactory)
    {
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