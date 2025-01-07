using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    [Inject] private PersistantStaticData _staticData;

    private void Start()
    {
        Debug.Log(_staticData);
    }

    public Decor SpawnDecor(Decor decorPrefab)
    {
        var decpr =  Instantiate(decorPrefab);
        decpr.Initialize(_staticData);

        return decpr;
    }
}
