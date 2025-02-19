using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    public List<FloorMarker> FloorMarkers = new();

    public SpaceDeterminantor(StateMachine stateMachine)
    {
        stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is DecorationState)
            FindDecorableSpace();
    }

    public void FindDecorableSpace()
    {
        var markers = GameObject.FindObjectsByType<FloorMarker>(FindObjectsSortMode.None);
        
        if (markers != null)
            FloorMarkers.Clear();
        foreach (var marker in markers)
        {
            FloorMarkers.Add(marker);
        }
    }
}
