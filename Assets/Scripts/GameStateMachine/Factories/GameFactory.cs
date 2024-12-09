using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IService
{
    public event Action PlayerCreated;

    public GameObject PlayerGameObject { get; private set; }

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    private readonly IAssets _assets;
    private readonly PersistantStaticData _staticData;
    private readonly PersistantPlayerStaticData _playerStaticData;

    public GameFactory(IAssets assets, PersistantStaticData staticData, PersistantPlayerStaticData playerStaticData)
    {
        _assets = assets;
        _staticData = staticData;
        _playerStaticData = playerStaticData;
    }

    public GameObject CreatePlayerAt(GameObject at, IInputService input)
    {
        PlayerGameObject = InstantiateRegistered(AssetPath.HeroPath, at.transform.position);
        PlayerGameObject.GetComponent<HeroMove>().Init(input);
        PlayerCreated?.Invoke();
        return PlayerGameObject;
    }

    public GameObject CreateHud()
    {
        var hud = InstantiateRegistered(AssetPath.HUDPath);
        return hud;
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
