using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DebugInstaller : MonoInstaller
{
    [SerializeField] private InputActionReference[] _tests;
    [Space]
    [SerializeField] private InputActionReference[] _mat_tests;
    [SerializeField] private CraftLootSettings[] _craftLootSettings;
    [Space]
    [SerializeField] private Decor[] _decorPrefabs;
    [SerializeField] private InputActionReference _allMatTest;

    public override void InstallBindings()
    {
        Container.Bind<CheatCodes>().FromNew().AsSingle().
            WithArguments(_tests, _mat_tests, _craftLootSettings, _decorPrefabs, _allMatTest).NonLazy();
    }
}