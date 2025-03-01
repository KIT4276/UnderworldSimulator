using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private PersistantStaticData _persistantStaticData;
    [SerializeField] private GameObject _workbenchPrefab;
    [SerializeField] private GameObject _decorFactiryPrefab;
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private GameObject _LootPrefab;
    [SerializeField] private GameObject _craftMenuPrefab;

    public override void InstallBindings()
    {
        InstallScriptableObjects();

        Container.Bind<InventorySystem>().FromComponentInNewPrefab(_inventoryPrefab).AsSingle().NonLazy();

        Container.Bind<DecorHolder>().FromNew().AsSingle().NonLazy();

        Container.Bind<WorkbenchSystem>().FromComponentInNewPrefab(_workbenchPrefab).AsSingle().NonLazy();

        Container.Bind<SpaceDeterminantor>().FromNew().AsSingle().NonLazy(); 

        Container.Bind<DecorFactory>().FromComponentInNewPrefab(_decorFactiryPrefab).AsSingle().NonLazy();
        Container.Bind<DecorationSystem>().FromNew().AsSingle().NonLazy();
        Container.Bind<LootSystem>().FromComponentInNewPrefab(_LootPrefab).AsSingle().NonLazy();

        Container.Bind<CraftSystem>().FromNew().AsSingle().NonLazy();
        Container.Bind<CraftMenu>().FromComponentInNewPrefab(_craftMenuPrefab).AsSingle().NonLazy();
    }

    private void InstallScriptableObjects()
    {
        Container.Bind<PersistantStaticData>().FromInstance(_persistantStaticData).AsSingle().NonLazy();
    }
}