using UnityEngine;

public class DecorationSystem 
{
  private DecorFactory _factory;

    private Decor _activeDecor;
    private object _mainCamera;

    public Decor ActiveDecor { get =>_activeDecor; }

    public DecorationSystem(DecorFactory factory)
    {
        _factory = factory;
        _factory.Initialize(this);
    }

    public void InstantiateDecor(Decor decorPrefab)
    {
        if (_activeDecor != null)
            GameObject.Destroy(_activeDecor.gameObject);

        _activeDecor = _factory.SpawnDecor(decorPrefab);
        ActivateDecor(_activeDecor);
    }

    private void ActivateDecor(Decor decor)
    {
        decor.PlacedAction += RemoveActiveDecor;
        decor.CanceledAcrion += RemoveDecor;
    }

    public void ReActivateDecor(Decor decor)
    {
        _activeDecor = decor;
        ActivateDecor(decor);
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

    private void CheckCamera()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }
}
