using UnityEngine;

public class DecorationState : IState
{
    private WorkbenchSystem _workbench;
    private readonly StateMachine _stateMachine;

    public DecorationState(WorkbenchSystem workbench, StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _workbench = workbench;

        Debug.Log(_stateMachine);
        Debug.Log(_workbench);
    }

    public void Enter()
    {
        _workbench.Activate();
        //Debug.Log("DecorationState Enter");
    }

    public void Exit()
    {
        _workbench.DeActivate();
        _stateMachine.Enter<GameLoopState>();
    }
}