using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private PersistantStaticData _persistantStaticData;

    public override void InstallBindings()
    {
        InstallScriptableObjects();
    }

    private void InstallScriptableObjects()
    {
        Container.Bind<PersistantStaticData>().FromInstance(_persistantStaticData).AsSingle().NonLazy();
    }
}