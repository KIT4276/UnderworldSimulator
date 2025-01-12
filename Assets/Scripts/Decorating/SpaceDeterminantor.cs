using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    private List<Floor> _floorObjects = new();
    private List<GreedHolder> _greedHolders = new();
    private IAssets _assets;

    public List<GreedHolder> GreedHolders { get => _greedHolders; }
    public List<Floor> FloorObjects { get => _floorObjects;  }

    public SpaceDeterminantor(IAssets assets)
    {
        _assets = assets;
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
            _floorObjects.Add(obj.GetComponent<Floor>());
            _greedHolders.Add(new GreedHolder(this, _assets));
        }
    }
}
