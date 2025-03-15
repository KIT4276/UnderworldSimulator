using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestsSystem 
{
    public List< Guest> Guests { get; private set; }

    private bool _isInited;
    private IAssets _assets;

    public event Action GuestsInstantiated;

    public GuestsSystem(StateMachine stateMachine, IAssets assets)
    {
        _assets = assets;
        stateMachine.ChangeStateAction += OnChangeState;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState && !_isInited)
        {
            Guests = new();
            DeterminanteGuests();
            _isInited = true;
        }
    }

    private void DeterminanteGuests()
    {
        GuestsPoint[] points = GameObject.FindObjectsByType<GuestsPoint>(FindObjectsSortMode.None);
        
        List<Guest> prefabs = new();

        Dictionary<Guest, string> prefabsDict = new();

        foreach (var prefabPath in AssetPath.GuestsPaths)
        {
            GameObject obj = Resources.Load<GameObject>(prefabPath);
            Guest prefab = obj.GetComponent<Guest>();

            if (prefab != null)
            {
                prefabs.Add(prefab);
                prefabsDict.Add(prefab, prefabPath);
            }
        }

        InstantiatrGuestsInPoints(points, prefabs, prefabsDict);
    }

    private void InstantiatrGuestsInPoints(GuestsPoint[] points, List<Guest> prefabs, Dictionary<Guest, string> prefabsDict)
    {
        foreach (var point in points)
        {
            foreach (var guestsPrefab in prefabs)
            {
                if (guestsPrefab.GuestsType == point.Type)
                {
                    GameObject guestObj = _assets.Instantiate(prefabsDict[guestsPrefab], point.transform.position);
                    guestObj.transform.parent = point.transform;
                    Guests.Add(guestObj.GetComponent<Guest>());
                }
            }
        }
        Sort();
        GuestsInstantiated?.Invoke();
    }

    private void Sort()
    {
      Guests = Guests.OrderByDescending(guest => guest.IsOpen).ToList();
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
