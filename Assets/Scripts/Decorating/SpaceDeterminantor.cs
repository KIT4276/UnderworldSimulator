using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    private List<Floor> _floorObjects = new();
    private List<GreedHolder> _greedHolders = new();
    private IAssets _assets;
    private readonly PersistantStaticData _persistantStaticData;

    public List<GreedHolder> GreedHolders { get => _greedHolders; }
    public List<Floor> FloorObjects { get => _floorObjects;  }

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
        Floor[] objects = GameObject.FindObjectsByType<Floor>(FindObjectsSortMode.None);
        foreach (var obj in objects)
        {
            var floor = obj.GetComponent<Floor>();
            _floorObjects.Add(floor);
            _greedHolders.Add(new GreedHolder(floor, _assets, _persistantStaticData));
        }
    }
}
