using System.Collections.Generic;
using UnityEngine;

public class GuestsSystem 
{
    public Guest[] Guests { get; private set; }

    private bool _isInited;
    private Guest[] _guestsPrefabs;
    private IAssets _assets;

    public GuestsSystem(StateMachine stateMachine, Guest[] guestsPrefabs, IAssets assets)
    {
        _guestsPrefabs = guestsPrefabs;
        _assets = assets;
        stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState && !_isInited)
        {
            DeterminanteGuestsPoints();
            _isInited = true;
        }
    }

    private void DeterminanteGuestsPoints()
    {
        GuestsPoint[] points = GameObject.FindObjectsByType<GuestsPoint>(FindObjectsSortMode.None);

        List<Guest> prefabs = new (); 

        Dictionary<Guest, string> prefabsDict = new();

        foreach(var prefabPath in AssetPath.GuestsPaths)
        {
            var prefab = Resources.Load<Guest>(prefabPath);
            if (prefab != null)
            {
                prefabs.Add(prefab);
                prefabsDict.Add(prefab, prefabPath);
            }
        }

        foreach (var point in points)
        {
            foreach(var guestsPrefab in prefabs)
            {
                if(guestsPrefab.GuestsType == point.Type)
                {
                    var guest = _assets.Instantiate(prefabsDict[guestsPrefab], point.transform.position);
                    guest.transform.parent = point.transform;

                }
            }
        }
    }
}
public enum GuestsType
{
    Wolf,
    Buffalo,
    Monkey,
    Hare,
    Bear,
}
