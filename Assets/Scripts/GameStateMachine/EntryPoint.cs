using UnityEngine;
using Zenject;

public class EntryPoint : MonoBehaviour
{
    [Inject] private readonly DiContainer _container;

    private void Start()
    {
        while (_container.Resolve<StateMachine>() == null)
            Debug.Log("wait for StateMachine");

        _container.Resolve<StateMachine>().Initialize();
    }
}
