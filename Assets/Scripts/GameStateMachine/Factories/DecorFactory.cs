using System;
using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;
    private IAssets _assets;
    private DecorationSystem _decorationSystem;

    public event Action<Decor> OnSpawned;

    [Inject]
    private void Construct(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder
        , IAssets assets)
    {
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
        _assets = assets;
    }

    public void Initialize(DecorationSystem decorationSystem) =>
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        Decor decor;

        if (!decorPrefab.gameObject.scene.IsValid())
        {
            decor = Instantiate(decorPrefab);
        }
        else
        {
            decor = decorPrefab;
            decor.gameObject.SetActive(true);
        }

        decor.Initialize(_staticData, _decorationSystem, _spaceDeterminantor, _assets);
        OnSpawned?.Invoke(decor);
        return decor;
    }


    public void DespawnDecor(Decor decor)
    {
        decor.gameObject.SetActive(false);
        decor.RemoveThisDecor();
        decor.transform.position = new Vector3(0, 0, 0);
    }
}
