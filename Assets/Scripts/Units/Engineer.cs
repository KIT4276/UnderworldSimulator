using UnityEngine;
using Zenject;

public class Engineer : MonoBehaviour
{
    [Inject] private StateMachine _stateMachine;

    public void EnterToWorkbench()
    {

    }
}
