using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private DecorationSystem _decorationSystem;

    [Inject]
    private void Construct(PersistantStaticData staticData) => 
        _staticData = staticData;

    public void Initialize(DecorationSystem decorationSystem) => 
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        var decpr = Instantiate(decorPrefab);
        decpr.Initialize(_staticData, _decorationSystem);

        return decpr;
    }
}
