using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    private List<GreedPolygonSplitter> _floorObjects = new();
    private List<GreedHolder> _greedHolders = new();
    private IAssets _assets;
    private readonly PersistantStaticData _persistantStaticData;

    public List<GreedHolder> GreedHolders { get => _greedHolders; }
    public List<GreedPolygonSplitter> FloorObjects { get => _floorObjects;  }

    public SpaceDeterminantor(IAssets assets, PersistantStaticData persistantStaticData)
    {
        _assets = assets;
        _persistantStaticData = persistantStaticData;
    }

    public void StartFind()
    {
        FindDecorableSpace();
    }

    public void FindDecorableSpace()
    {
        GreedPolygonSplitter[] objects = GameObject.FindObjectsByType<GreedPolygonSplitter>(FindObjectsSortMode.None);
        
        foreach (var obj in objects)
        {
            //var floor = obj.GetComponent<GreedPolygonSplitter>();
            _floorObjects.Add(obj);
            _greedHolders.Add(new GreedHolder(obj, _assets, _persistantStaticData));
        }
    }
}
