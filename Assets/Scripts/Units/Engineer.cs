using UnityEngine;
using Zenject;

public class Engineer : InteractableObstacle
{
    [Inject] private WorkbenchSystem _workbenchSystem;
    [Inject] private StateMachine _machine;
    [Inject] private GameLoopState _gameLoopState;

    protected override void Interac()
    {
        if (_machine.ActiveState != _gameLoopState) 
            return;

        _sign.SetActive(false);
        _workbenchSystem.ActivateWorkbench();
    }
}
