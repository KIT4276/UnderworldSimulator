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
    }

    private void RemoveActiveDecor()
    {
        _activeDecor = null;
        //_activeDecor.PlacedAcrion -= RemoveActiveDecor;
    }
}
