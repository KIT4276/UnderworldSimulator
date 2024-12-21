using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller, ICoroutineRunner
{
    [SerializeField] private GameObject _entryPointPrefab;
    [SerializeField] private GameObject _curtainPrefab;

    private const string Curtain = "_curtain";
    private const string Infrastructure = "Infrastructure";
    private const string EntryPoint = "EntryPoint";

    public override void InstallBindings()
    {
        InstallInputService();

        this.gameObject.SetActive(true);
        Container.BindInterfacesAndSelfTo<ICoroutineRunner>().FromInstance(this).AsSingle();

        InstallSceneLoader();

        BindFactories();
        BindServices();

        BindEntryPoint();
    }

    private void BindEntryPoint()
    {
        Container.BindInterfacesAndSelfTo<EntryPoint>().FromComponentInNewPrefab(_entryPointPrefab).
            WithGameObjectName(EntryPoint).UnderTransformGroup(Infrastructure).AsSingle().NonLazy();
    }

    private void BindServices()
    {
        Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromComponentInNewPrefab(_curtainPrefab).
            WithGameObjectName(Curtain).UnderTransformGroup(Infrastructure).AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<AssetsProvider>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PersistantProgressService>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveLoadService>().FromNew().AsSingle().NonLazy();
    }

    private void BindFactories()
    {
        Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle().NonLazy();
    }

    private void InstallSceneLoader()
    {
        Container.Bind<SceneLoader>().FromNew().AsSingle().WithArguments(this).NonLazy();
    }

    private void InstallInputService()
    {
        //IInputService input = DefineInputService();
        //Container.BindInterfacesAndSelfTo<IInputService>().FromInstance(input).AsSingle().NonLazy();
    }


}