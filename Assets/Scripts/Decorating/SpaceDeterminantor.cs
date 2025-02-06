using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    //private List<GreedPolygonSplitter> _floorObjects = new();
    //private List<GreedHolder> _greedHolders = new();
    private IAssets _assets;
    private readonly PersistantStaticData _persistantStaticData;

    //public List<GreedHolder> GreedHolders { get => _greedHolders; }
    //public List<GreedPolygonSplitter> FloorObjects { get => _floorObjects;  }

    public List<FloorMarker> FloorMarkers = new();

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
        var markers = GameObject.FindObjectsByType<FloorMarker>(FindObjectsSortMode.None);
        
        foreach (var marker in markers)
        {
            FloorMarkers.Add(marker);
        }
    }
}
