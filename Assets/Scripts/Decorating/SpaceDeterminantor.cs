using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    private List<Floor> _floorObjects = new();

    public List<Floor> FloorObjects { get => _floorObjects;  }

    public event Action Found;

    public void StartFind()
    {
        FindDecorableSpace();
    }

    public void FindDecorableSpace()
    {
        Floor[] objects = GameObject.FindObjectsByType<Floor>(FindObjectsSortMode.None);
        
        foreach (var obj in objects)
            _floorObjects.Add(obj.GetComponent<Floor>());

        Found?.Invoke();
    }
}
