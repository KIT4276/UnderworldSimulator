using Zenject;

public class Engineer : InteractableObstacle
{
    [Inject] private StateMachine _machine;

    private void Start()
    {
        _gameLoopState.GameLoopStateEnter += ConditionalActivate;
        if(_machine != null)
        {
            _machine.ChangeStateAction += OnStateChange;
        }
    }

    private void OnStateChange(IExitableState state)
    {
       if(state is InventoryState )
        {
            if (_isInTrigger)
            {
                Activate();
            }
        }
    }

    private void ConditionalActivate()
    {
        if (_machine.PredioslyState is WorkbenchState)
            Activate();
    }

    protected override void Interac()
    {
        if (_machine.ActiveState is GameLoopState || _machine.ActiveState is InventoryState)
        {

            _sign.SetActive(false);
            _machine.Enter<WorkbenchState>();
        }

    }

    private void OnDestroy()
    {
        _gameLoopState.GameLoopStateEnter -= ConditionalActivate;
    }
}
