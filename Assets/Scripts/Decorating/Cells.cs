using System;
using UnityEngine;
using Zenject;

public class Cells : MonoBehaviour
{
    [SerializeField] private GameObject _cells;
    private StateMachine _stateMachine;


    // private DecorationState _decorationState;
    //private GameLoopState _gameLoopState;
    //private WorkbenchState _workbenchState;

    [Inject]
    public void Construct(/*DecorationState decorationState, GameLoopState gameLoopState, WorkbenchState workbenchState*/ StateMachine stateMachine)
    {
        _cells.SetActive(true);
        // _decorationState = decorationState;
        //_gameLoopState = gameLoopState;
        //_workbenchState = workbenchState;
        _stateMachine = stateMachine;
        _stateMachine.ChangeStateAction += OnChangeState;
        //_workbenchState.WorkbenchStateEnter += ActivateCells;
        ////_decorationState.DecorationStateEnter += ActivateCells;
        //_gameLoopState.GameLoopStateEnter += OnGameLoopStateEnter;
        //_decorationState.DecorationStateExit += OnDecorStateExit;
    }

    private void OnChangeState(IExitableState state)
    {
        if(state is DecorationState || state is WorkbenchState)
        {
            _cells.SetActive(true);
        }
        else
        {
            _cells.SetActive(false);
        }
    }

    //private void OnGameLoopStateEnter()
    //{
    //    _cells.SetActive(false);
    //}

    //private void ActivateCells()
    //{
    //    _cells.SetActive(true);
    //}

    private void OnDisable()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
        //_workbenchState.WorkbenchStateEnter -= ActivateCells;
        ////_decorationState.DecorationStateEnter -= ActivateCells;
        //_gameLoopState.GameLoopStateEnter -= OnGameLoopStateEnter;
    }
}
