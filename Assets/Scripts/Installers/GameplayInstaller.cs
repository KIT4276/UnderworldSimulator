using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private PersistantStaticData _persistantStaticData;
    [SerializeField] private PlayerInput _playerInputPrefab;

    public override void InstallBindings()
    {
        InstallScriptableObjects();

        InstallInput();
    }

    private void InstallInput()
    {
        Container.Bind<PlayerInput>().FromInstance(_playerInputPrefab).AsSingle().NonLazy();
    }

    private void InstallScriptableObjects()
    {
        Container.Bind<PersistantStaticData>().FromInstance(_persistantStaticData).AsSingle().NonLazy();
    }
}