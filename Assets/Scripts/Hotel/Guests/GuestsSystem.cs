using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestsSystem : ISavedProgress
{
    private readonly GuestsSpawner _spawner;
    private readonly IAssets _assets;

    private bool _isInited;

    public List<Guest> Guests { get; private set; }

    private readonly StateMachine _stateMachine;

    public event Action GuestsChanged;

    public GuestsSystem(StateMachine stateMachine, IAssets assets)
    {
        Guests = new();
        _stateMachine = stateMachine;

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
        progress.Guests = Guests;
    }

    public void LoadProgress(PlayerProgress progress)
    {
        Guests = progress.Guests;
        _spawner.SpawnGuests(Guests, _assets, _stateMachine);
        Sort();
        GuestsChanged?.Invoke();
    }

    public Guest FindGuestByRoom(Room room)
    {
        foreach (var guest in Guests)
        {
            if (guest.IsAvailable && guest.Room == room)
            {
                return guest;
            }

        }
        return null;
    }

    public Guest FindGuestByType(GuestsType type)
    {
        Guest currGuest = Guests[0];

        foreach (var guest in Guests)
        {
            if (guest.Type == type)
            {
                currGuest = guest;
            }
        }
        return currGuest;
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
