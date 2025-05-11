using System;
using System.Diagnostics;
using Zenject;

public class Engineer : InteractableObstacle
{
    [Inject] private StateMachine _machine;

    private void Start()
    {
        _gameLoopState.GameLoopStateEnter += ConditionalActivate;
    }

    private void ConditionalActivate()
    {
        if (_machine.PredioslyState is WorkbenchState)
            Activate();
    }

    protected override void Interac()
    {
        if (_machine.ActiveState != _gameLoopState)
            return;

        _sign.SetActive(false);
        _machine.Enter<WorkbenchState>();

    }

    private void OnDestroy()
    {
        _gameLoopState.GameLoopStateEnter -= ConditionalActivate;
    }
}
