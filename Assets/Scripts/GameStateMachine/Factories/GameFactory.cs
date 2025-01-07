using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameFactory : IService
{
    private DiContainer _container;

    public event Action PlayerCreated;

    public GameObject PlayerGameObject { get; private set; }

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    public HeroMove HeroMove { get; private set; }
    public CameraMove CameraMove { get; private set; }

    private readonly IAssets _assets;
    private readonly PersistantStaticData _staticData;//will be used later
    private readonly PersistantPlayerStaticData _playerStaticData;//will be used later

    public GameFactory(IAssets assets, PersistantStaticData staticData, PersistantPlayerStaticData playerStaticData,
        DiContainer container)
    {
        _assets = assets;
        _staticData = staticData;
        _playerStaticData = playerStaticData;
        _container = container;
    }

    public GameObject CreatePlayerAt(GameObject at)
    {
        PlayerGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
        HeroMove = PlayerGameObject.GetComponent<HeroMove>();
        HeroMove.Init();
        InitCamera();
        _container.Bind<HeroMove>().AsSingle();
        
        _container.Bind<PlayerInput>().FromInstance(PlayerGameObject.GetComponent<PlayerInput>()).AsSingle().NonLazy();

        PlayerCreated?.Invoke();
        return PlayerGameObject;
    }

    private void InitCamera()
    {
        CameraMove = PlayerGameObject.GetComponentInChildren<CameraMove>();

        PlayerGameObject.GetComponentInChildren<CinemachinePositionComposer>().transform.parent = null;

        CameraMove.transform.parent = null;
    }

    public StartMenu CreateStartMenu() =>
        _assets.Instantiate(AssetPath.StartMenuPath).GetComponent<StartMenu>();

    public void CleanUp()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath, at);

        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath);

        RegisterProgressWatchers(gameObject);
        return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            Register(progressReader);
    }

    public void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);

        ProgressReaders.Add(progressReader);
    }
}
