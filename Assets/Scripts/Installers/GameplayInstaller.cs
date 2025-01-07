using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private PersistantStaticData _persistantStaticData;
    [SerializeField] private GameObject _workbenchPrefab;
    [SerializeField] private GameObject _decorFactiryPrefab;

    public override void InstallBindings()
    {
        InstallScriptableObjects();

        Container.Bind<WorkbenchSystem>().FromComponentInNewPrefab(_workbenchPrefab).AsSingle().NonLazy();
        Container.Bind<DecorFactory>().FromComponentInNewPrefab(_decorFactiryPrefab).AsSingle().NonLazy();
        Container.Bind<DecorationSystem>().FromNew().AsSingle().NonLazy();
    }

    private void InstallScriptableObjects()
    {
        Container.Bind<PersistantStaticData>().FromInstance(_persistantStaticData).AsSingle().NonLazy();
    }
}