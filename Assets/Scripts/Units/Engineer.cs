using Zenject;

public class Engineer : InteractableObstacle
{
    [Inject] private WorkbenchSystem _workbenchSystem;
    [Inject] private StateMachine _machine;
   

    //[Inject]
    //private void Construct(WorkbenchSystem workbenchSystem, StateMachine machine, GameLoopState gameLoopState)
    //{
    //    _workbenchSystem = workbenchSystem;
    //    _machine = machine;
    //    _gameLoopState = gameLoopState;
    //    _gameLoopState.EnterGameLoopState += OnGameLoopStateEnter;
    //}

    protected override void Interac()
    {
        if (_machine.ActiveState != _gameLoopState)
            return;

        _sign.SetActive(false);
        _workbenchSystem.ActivateWorkbench();
       
    }

    
}
