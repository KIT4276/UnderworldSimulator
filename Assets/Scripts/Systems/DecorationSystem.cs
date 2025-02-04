using System;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSystem
{
    private DecorFactory _factory;
    private Decor _activeDecor;
    private object _mainCamera;
    private List<Decor> _allDecorInTheScene = new();

    public Decor ActiveDecor { get => _activeDecor; }
    public bool CanPlace { get; private set; }

    public event Action<Decor> RemoveDecorAction;
    public event Action<Decor> TryToRemoveDecorAction;

    public DecorationSystem(DecorFactory factory)
    {
        _factory = factory;
        _factory.Initialize(this);
    }

    public void BanActions() => 
        CanPlace = false;

    public void AllowActions() =>
        CanPlace = true;

    public void SetIsOnDecorState(bool isOnDecorState)
    {
        foreach (var decor in _allDecorInTheScene)
        {
            if (decor != null)
                decor.SetIsOnDecorState(isOnDecorState);
        }
    }

    public void InstantiateDecor(Decor decorPrefab)
    {
        if (_activeDecor != null)
            _factory.DespawnDecor(_activeDecor);
        _activeDecor = _factory.SpawnDecor(decorPrefab);
        ActivateDecor(_activeDecor);
    }

    private void ActivateDecor(Decor decor)
    {
        decor.SetIsOnDecorState(true);
        decor.PlacedAction += PlaceActiveDecor;
    }

    public void ReActivateDecor(Decor decor)
    {
        _activeDecor = decor;
        ActivateDecor(decor);
    }


    public void TryToRemoveDecor(Decor decor)
    {
        if (_allDecorInTheScene.Contains(_activeDecor))
            _allDecorInTheScene.Remove(_activeDecor);

        if (decor == _activeDecor)
        {
            TryToRemoveDecorAction?.Invoke(_activeDecor);
        }
    }

    public void ReturtDecorToInventory()
    {
        _factory.DespawnDecor(_activeDecor);

        RemoveDecorAction?.Invoke(_activeDecor);
        _activeDecor = null;
    }

    private void PlaceActiveDecor()
    {
        _allDecorInTheScene.Add(_activeDecor);
        _activeDecor = null;
    }

    private void CheckCamera()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }
}
