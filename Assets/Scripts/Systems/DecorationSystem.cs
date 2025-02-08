using System;

public class DecorationSystem
{
    private DecorHolder _decorHolder;
    private DecorFactory _factory;
    private object _mainCamera;

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

    public bool SpawnDecorIfCan(Decor decorPrefab)
    {
        if (_decorHolder.ActiveDecor != null)
            return false;

        else
        {
            var decor = _factory.SpawnDecor(decorPrefab);
            decor.SetIsOnDecorState(true);
            _decorHolder.SetActiveDecor(decor);
            return true;
        }
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



    public void TryToRemoveDecor(Decor decor)
    {
        TryToRemoveDecorAction?.Invoke(decor);
    }


    public void ReturtDecorToInventory(Decor decor)
    {
        _factory.OnRemoveDecor(decor);
        _decorHolder.DeActiveDecor();
        //decor.RemoveThisDecor();
    }
}
