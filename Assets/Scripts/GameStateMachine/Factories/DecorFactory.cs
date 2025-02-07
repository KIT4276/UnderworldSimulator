using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;
    private DecorationSystem _decorationSystem;

    private int _currentID;

    [Inject]
    private void Construct(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, IAssets assets, DecorHolder decorHolder)
    {
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
    }

    public void Initialize(DecorationSystem decorationSystem) =>
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        Decor decor;
        _currentID++;

        if (!decorPrefab.gameObject.scene.IsValid())
        {
            //Debug.Log("NONONONONON_________________");
            decor = Instantiate(decorPrefab);
        }
        else
        {
            //Debug.Log("IsValid()");
            decor = decorPrefab;
            decor.gameObject.SetActive(true);
        }
        
        decor.Initialize(_staticData, _decorationSystem, _spaceDeterminantor, _currentID, _decorHolder);
        return decor;
    }


    public void OnRemoveDecor(Decor decor)
    {
        decor.RemoveThisDecor();
        decor.transform.position = new Vector3(0, 0, 0);
        decor.gameObject.SetActive(false);
    }
}
