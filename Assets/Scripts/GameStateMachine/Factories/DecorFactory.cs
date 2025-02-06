using System;
using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorationSystem _decorationSystem;
    private Decor _removableDecor;

    //public event Action<Decor> OnSpawned;

    [Inject]
    private void Construct(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, IAssets assets)
    {
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
    }

    public void Initialize(DecorationSystem decorationSystem) =>
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        Decor decor;

        if (!decorPrefab.gameObject.scene.IsValid())
        {
            Debug.Log("NONONONONON_________________");
            decor = Instantiate(decorPrefab);
        }
        else
        {
            Debug.Log("IsValid()");
            decor = decorPrefab;
            decor.gameObject.SetActive(true);
        }
        
        decor.Initialize(_staticData, _decorationSystem, _spaceDeterminantor);
        return decor;
    }


    public void DespawnDecor(Decor decor)
    {
        _removableDecor = decor;
        decor.Removed += OnRemoveDecor;
    }

    private void OnRemoveDecor()
    {
        _removableDecor.gameObject.SetActive(false);
        _removableDecor.RemoveThisDecor();
        _removableDecor.transform.position = new Vector3(0, 0, 0);

    }
}
