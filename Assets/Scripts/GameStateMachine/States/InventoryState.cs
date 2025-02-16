public class InventoryState : IState
{
    private readonly GameFactory _gameFactory;

    public InventoryState(GameFactory gameFactory)
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