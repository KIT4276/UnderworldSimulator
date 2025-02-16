using System;
using System.Collections.Generic;

public class StateMachine
{
    private readonly StateFactory _stateFactory;

    private Dictionary<Type, IExitableState> _states;

    private IExitableState _activeState;
    private bool _isInited;

    public IExitableState ActiveState {  get => _activeState; } 

    public StateMachine(StateFactory stateFactory) =>
        _stateFactory = stateFactory;

    public void Initialize()
    {
        if (_isInited) return;

        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = _stateFactory
            .CreateState<BootstrapState>(),
            [typeof(LoadProgressState)] = _stateFactory
            .CreateState<LoadProgressState>(),
            [typeof(LoadLevelState)] = _stateFactory
            .CreateState<LoadLevelState>(),
            [typeof(GameLoopState)] = _stateFactory
            .CreateState<GameLoopState>(),
            [typeof(WorkbenchState)] = _stateFactory
            .CreateState<WorkbenchState>(),
            [typeof(DecorationState)] = _stateFactory
            .CreateState < DecorationState>(),
            [typeof(LootState)] = _stateFactory
            .CreateState<LootState>(),
            [typeof(InventoryState)] = _stateFactory
            .CreateState<InventoryState>(),
        };
        Enter<BootstrapState>();
        _isInited = true;
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();

        //Debug.Log( ActiveState);
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangeState<TState>();
        state.Enter(payload);

       // Debug.Log("Enter To " + ActiveState);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _activeState?.Exit();

        TState state = GetState<TState>();
        _activeState = state;

        return state;
    }

    public TState GetState<TState>() where TState : class, IExitableState =>
         _states[typeof(TState)] as TState;
}
