using System;

public class LootState : IState
{
    private readonly GameFactory _gameFactory;

    //public event Action LootStateEnter;
    //public event Action LootStateExit;

    public LootState (GameFactory gameFactory)
    {
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
        //LootStateEnter?.Invoke();
        _gameFactory.HeroMove.Immobilize();
    }

    public void Exit()
    {
        //LootStateExit?.Invoke();
        //_gameFactory.HeroMove.Mobilize();
    }
}

    