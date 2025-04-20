using UnityEngine;
using Zenject;

public class Walls : MonoBehaviour
{
    [SerializeField] private GameObject[] _bigWalls;
    [SerializeField] private GameObject[] _smallWalls;
    [SerializeField] private GameObject[] _roof;

    private WallsSystem _wallsSystem;
    private StateMachine _stateMachine;

    private bool _isInit;

    [Inject]
    private void Constrtuct(WallsSystem wallsSystem, StateMachine stateMachine)
    {
        _wallsSystem = wallsSystem;
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        _stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState && !_isInit)
        {
            _wallsSystem.InitWithWalls(_smallWalls, _bigWalls, _roof);
            _isInit = true;
            //Debug.Log("OnChangeState GameLoopState");
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
