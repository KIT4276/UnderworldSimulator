using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestsSystem : ISavedProgress
{
    private readonly GuestsSpawner _spawner;
    private readonly IAssets _assets;

    //private List<Guest> _guests = new();
    private bool _isInited;

    public List<Guest> Guests { get; private set; }//{ get => _guests; }

    public event Action GuestsChanged;

    public GuestsSystem(StateMachine stateMachine, IAssets assets)
    {
        Guests = new();

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
        Guests = Guests.OrderByDescending(guest => guest.IsAvailable).ToList();
    }

    public void SaveProgress(PlayerProgress progress)
    {
        Debug.Log("SaveProgress");
        progress.Guests = Guests;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        Guests = progress.Guests;
        _spawner.SpawnGuests(Guests, _assets);
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
