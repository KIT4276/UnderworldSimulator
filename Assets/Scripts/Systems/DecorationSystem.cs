using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class DecorationSystem
{
  // private InputAction _escapeAction;
    private DecorHolder _decorHolder;
    private DecorFactory _factory;
   // private StateMachine _stateMachine;

    public event Action<Decor> TryToRemoveDecorAction;

    public DecorationSystem(DecorFactory factory, DecorHolder decorHolder /*,PlayerInput playerInput, *//*StateMachine stateMachine*/)
    {
        //_escapeAction = playerInput.actions["Escape"];

        _decorHolder = decorHolder;
        _factory = factory;
        //_stateMachine = stateMachine;
        _factory.Initialize(this);

        //_escapeAction.performed += OnEscape;
    }

    //private void OnEscape(InputAction.CallbackContext context)
    //{
    //    _stateMachine.Enter<WorkbenchState>();
    //}

    public void SetCanDecorate(bool isOnDecorState)// ьб понадобится
    {
        //foreach (var decor in _decorHolder.GetDecorsInScene())
        //{
        //    decor.SetIsCanDecorate(isOnDecorState);
        //}
    }

    public bool ActivateDecorIfCan(Decor decor)
    {
        _decorHolder.SetActiveDecor(decor);
        return true;
    }

    public void SpawnDecorIfCan(Decor decorPrefab)
    {
        if (_decorHolder.ActiveDecor != null)
            TryToRemoveDecor(decorPrefab);

        var decor = _factory.SpawnDecor(decorPrefab);
        decor.SetIsCanDecorate(true);
        _decorHolder.SetActiveDecor(decor);
    }

    public void InstanriateDecor(Decor decor)
    {
        Debug.Log("InstanriateDecor");
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
