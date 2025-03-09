using System;
using UnityEngine;

public class DecorationSystem
{
    private DecorHolder _decorHolder;
    private DecorFactory _factory;

    public event Action<Decor> TryToRemoveDecorAction;

    public DecorationSystem(DecorFactory factory, DecorHolder decorHolder /*,PlayerInput playerInput, *//*StateMachine stateMachine*/)
    {
        _decorHolder = decorHolder;
        _factory = factory;
        _factory.Initialize(this);
    }

    public void SetCanDecorate(bool isOnDecorState)// ьб понадобится
    {
    }

    public bool ActivateDecorIfCan(Decor decor)
    {
        _decorHolder.SetActiveDecor(decor);
        return true;
    }

    public void SpawnDecorIfCan(Decor decorPrefab)
    {
        if (_decorHolder.ActiveDecor != null)
            // TryToRemoveDecor(decorPrefab);
            return;
       // Debug.Log("OnButtonClick");
        var decor = _factory.SpawnDecor(decorPrefab);
        decor.SetIsCanDecorate(true);
        _decorHolder.SetActiveDecor(decor);
    }

    public void InstanriateDecor(Decor decor)
    {
      ////  Debug.Log("InstanriateDecor");
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
        //Debug.Log("TryToRemoveDecor");
        TryToRemoveDecorAction?.Invoke(decor);
    }

    public void ReturtDecorToInventory(Decor decor)
    {
        //Debug.Log("ReturtDecorToInventory in DecorSyst");
        _factory.OnRemoveDecor(decor);
        _decorHolder.DeActiveDecor();
    }
}
