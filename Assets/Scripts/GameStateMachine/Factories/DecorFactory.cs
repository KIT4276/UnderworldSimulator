using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorationSystem _decorationSystem;

    [Inject]
    private void Construct(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor)
    {
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
    }

    public void Initialize(DecorationSystem decorationSystem) => 
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        var decpr = Instantiate(decorPrefab);
        decpr.Initialize(_staticData, _decorationSystem, _spaceDeterminantor);

        return decpr;
    }
}
