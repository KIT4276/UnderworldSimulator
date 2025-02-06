using System;

public class DecorationSystem
{
    private DecorHolder _decorHolder;
    private DecorFactory _factory;
    private object _mainCamera;
    //public bool CanPlace { get; private set; }

    //public event Action<Decor> RemoveDecorAction;
    public event Action<Decor> TryToRemoveDecorAction;

    public DecorationSystem(DecorFactory factory, DecorHolder decorHolder)
    {
        _decorHolder = decorHolder;
        _factory = factory;
        _factory.Initialize(this);
    }

    public void SetIsOnDecorState(bool isOnDecorState)
    {
        foreach (var decor in _decorHolder.GetDecorsInScene())
        {
            decor.SetIsOnDecorState(isOnDecorState);
        }
    }

    public bool ActivateDecorIfCan(Decor decor)
    {
        if (_decorHolder.ActiveDecor != null)
            return false;
        else
        {
            _decorHolder.SetActiveDecor(decor);
            return true;
        }
    }

    public void SpawnDecorIfCan(Decor decorPrefab)
    {
        if (_decorHolder.ActiveDecor != null) return;

        var decor = _factory.SpawnDecor(decorPrefab);
        decor.SetIsOnDecorState(true);
        _decorHolder.SetActiveDecor(decor);
    }

    public void DeSpawnDecorIfCan(Decor decorPrefab)
    {
        //todo
    }

    public void InstanriateDecor(Decor decor)
    {
        _decorHolder.AddInstalledDecor(decor);
    }

    public void BanActions()
    {
        foreach (var decor in _decorHolder.GetDecorsInScene())
        {
            decor?.BanActions();
        }
    }

    public void AllowActions() 
        {
        foreach (var decor in _decorHolder.GetDecorsInScene())
        {
            decor?.AllowActions();
        }
    }


    //private void ActivateDecor(Decor decor)
    //{
    //    decor.SetIsOnDecorState(true);
    //    decor.PlacedAction += PlaceActiveDecor;
    //}

    //public void ReActivateDecor(Decor decor)
    //{
    //    _activeDecor = decor;
    //    ActivateDecor(decor);
    //}


    public void TryToRemoveDecor(Decor decor)
    {
        TryToRemoveDecorAction?.Invoke(decor);
            //_decorHolder.DeActiveDecor();
    }


    public void ReturtDecorToInventory(Decor decor)
    {
        _factory.DespawnDecor(decor);
        _decorHolder.DeActiveDecor();
        decor.RemoveThisDecor();
    }

    //private void PlaceActiveDecor()
    //{
    //    _allDecorInTheScene.Add(_activeDecor);
    //    _activeDecor = null;
    //}

    //private void CheckCamera()
    //{
    //    if (_mainCamera == null)
    //        _mainCamera = Camera.main;
    //}
}
