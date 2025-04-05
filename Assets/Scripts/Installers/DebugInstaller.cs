using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DebugInstaller : MonoInstaller
{
    [SerializeField] private InputActionReference _test_1;
    [SerializeField] private InputActionReference _test_2;
    [SerializeField] private InputActionReference _test_3;
    [SerializeField] private InputActionReference _test_4;
    [SerializeField] private InputActionReference _test_5;
    [SerializeField] private Decor[] _decorPrefabs;

    public override void InstallBindings()
    {
        Container.Bind<CheatCodes>().FromNew().AsSingle().
            WithArguments(_test_1, _test_2, _test_3, _test_4, _test_5, _decorPrefabs).NonLazy();
    }
}