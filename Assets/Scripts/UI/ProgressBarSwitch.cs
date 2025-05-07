using UnityEngine;
using Zenject;

public class ProgressBarSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _progressBar;

    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is DecorationState || state is WorkbenchState || state is CraftState)
        {
            _progressBar.SetActive(true);
        }
        else
        {
            _progressBar.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
    }
}
