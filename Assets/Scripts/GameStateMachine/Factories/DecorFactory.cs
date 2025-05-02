using UnityEngine;
using Zenject;

public class DecorFactory : MonoBehaviour
{
    private PersistantStaticData _staticData;
    private SpaceDeterminantor _spaceDeterminantor;
    private DecorHolder _decorHolder;
    private StateMachine _stateMachine;
    private DrawingData _drawingData;
    private DecorationSystem _decorationSystem;

    private int _currentID = 1;

    [Inject]
    private void Construct(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, IAssets assets, 
        DecorHolder decorHolder, StateMachine stateMachine, DrawingData drawingData)
    {
        _staticData = staticData;
        _spaceDeterminantor = spaceDeterminantor;
        _decorHolder = decorHolder;
        _stateMachine = stateMachine;
        _drawingData = drawingData;
    }

    public void Initialize(DecorationSystem decorationSystem) =>
        _decorationSystem = decorationSystem;

    public Decor SpawnDecor(Decor decorPrefab)
    {
        Decor decor;

        if (!decorPrefab.gameObject.scene.IsValid())
        {
            decor = Instantiate(decorPrefab);
            //Debug.Log("SpawnDecor No Valid");
        }
        else
        {
            //Debug.Log("SpawnDecor Valid");
            decor = decorPrefab;
            decor.gameObject.SetActive(true);
        }
        decor.gameObject.SetActive(true) ;

        string name = string.Empty;
        
        foreach(var draw in _drawingData.Drawings)
        {
            if(draw.Decor.DecorType == decor.DecorType)
            {
                name = draw.Name;
                break;
            }
        }

        decor.Initialize(_staticData, _decorationSystem, _spaceDeterminantor, _currentID, _decorHolder, _stateMachine, name);
        _currentID++;
        return decor;
    }


    public void OnRemoveDecor(Decor decor)
    {
        decor.RemoveThisDecor();
        decor.transform.position = new Vector3(0, 0, 0);
        decor.gameObject.SetActive(false);
    }
}
