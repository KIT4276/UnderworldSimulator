using System;
using UnityEngine;
using Zenject;

public class DecorationSystem 
{
    [Inject] private DecorFactory _factory;

    private Decor _activeDecor;

    public void InstantiateDecor(Decor decorPrefab)
    {
        if (_activeDecor != null)
            GameObject.Destroy(_activeDecor.gameObject);
        
            _activeDecor = _factory.SpawnDecor(decorPrefab);
        _activeDecor.PlacedAcrion += RemoveActiveDecor;
        _activeDecor.CanceledAcrion += RemoveDecor;
    }

    private void RemoveDecor()
    {
        GameObject.Destroy(_activeDecor.gameObject);

        //todo Return decor to inventory
    }

    private void RemoveActiveDecor()
    {
        _activeDecor = null;
    }
}
