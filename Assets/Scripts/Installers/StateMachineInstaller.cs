using UnityEngine;
using Zenject;

public class StateMachineInstaller : MonoInstaller
{
    [SerializeField] private PersistantPlayerStaticData _persistantPlayerStaticData;

    public override void InstallBindings()
    {
        Container.Bind<PersistantPlayerStaticData>().FromInstance(_persistantPlayerStaticData).AsSingle().NonLazy();

        Container.Bind<BootstrapState>().AsSingle().NonLazy();
        Container.Bind<LoadProgressState>().AsSingle().NonLazy();
        Container.Bind<LoadLevelState>().AsSingle().NonLazy();
        Container.Bind<GameLoopState>().AsSingle().NonLazy();

        Container
          .BindInterfacesAndSelfTo<StateMachine>()
          .AsSingle();
    }
}