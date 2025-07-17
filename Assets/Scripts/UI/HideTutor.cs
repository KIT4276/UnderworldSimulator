using UnityEngine;
using Zenject;

public class HideTutor : MonoBehaviour
{
    [SerializeField]private GameObject _tutorCanvas;

    private StateMachine _stateMachine;

    [Inject]
    private void Construct(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        if (_stateMachine!= null && _stateMachine.IsTests)
        {
            _tutorCanvas.SetActive(false);
        }
    }
}
