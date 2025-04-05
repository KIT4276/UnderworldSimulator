using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDeterminantor
{
    public List<FloorMarker> FloorMarkers = new();

    public event Action Find;

    private bool _iinited;

    public SpaceDeterminantor(StateMachine stateMachine)
    {
        stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (!_iinited)
        {
            if (state is DecorationState || state is WorkbenchState || state is CraftState)
                FindDecorableSpace();
        }
    }

    public void FindDecorableSpace()
    {
        var markers = GameObject.FindObjectsByType<FloorMarker>(FindObjectsSortMode.None);

        if (markers != null)
            FloorMarkers.Clear();
        foreach (var marker in markers)
        {
            FloorMarkers.Add(marker);
            marker.Init();
        }
        //Debug.Log(FloorMarkers.Count);
        SortMarkers();
        Find?.Invoke();
        _iinited = true;
    }

    private void SortMarkers()
    {
        if (FloorMarkers == null || FloorMarkers.Count == 0)
            return;

        FloorMarkers.Sort((a, b) => a.ID.CompareTo(b.ID));
    }
}
