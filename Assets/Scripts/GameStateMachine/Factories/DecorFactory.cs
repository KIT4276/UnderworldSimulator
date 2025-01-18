using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;
    private DecorationSystem _decorationSystem;

    [Inject]
    private void Construct(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder)
    {
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
    }

    public void Initialize(DecorationSystem decorationSystem) => 
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        var decpr = Instantiate(decorPrefab);
        decpr.Initialize(_staticData, _decorationSystem, _spaceDeterminantor, _decorHolder);

        return decpr;
    }

    public void DespawnDecor(Decor decor)
    {
        GameObject.Destroy(decor.gameObject);
    }
}
