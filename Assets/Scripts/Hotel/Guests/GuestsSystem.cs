using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestsSystem : ISavedProgress
{
    private readonly GuestsSpawner _spawner;
    private readonly IAssets _assets;

    private List<Guest> _guests = new();
    private bool _isInited;

    public List<Guest> Guests { get => _guests; }

    public event Action GuestsChanged;

    public GuestsSystem(StateMachine stateMachine, IAssets assets)
    {
        _spawner = new();
        _assets = assets;
        stateMachine.ChangeStateAction += OnChangeState;

    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState && !_isInited)
        {
            _isInited = true;
        }
    }

    private void Sort()
    {
      _guests = _guests.OrderByDescending(guest => guest.IsAvailable).ToList();
    }

    public void SaveProgress(PlayerProgress progress)
    {
        Debug.Log("SaveProgress");
        progress.Guests = _guests;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        _guests = progress.Guests;
        _spawner.SpawnGuests(_guests, _assets);
        Sort();
        GuestsChanged?.Invoke();
    }
}
public enum GuestsType
{
    Wolf,
    Bull,
    Monkey,
    Rabbit,
    Bear,
}
