using System;
using UnityEngine;

public class TasksSystem 
{
    private readonly RoomsSystem _roomsSystem;
    private readonly GuestsSystem _guestsSystem;
    private readonly TasksHandler _tasksHandler;

    public TasksSystem(RoomsSystem roomsSystem, GuestsSystem guestsSystem, TasksHandler tasksHandler)
    {
        _roomsSystem = roomsSystem;
        _guestsSystem = guestsSystem;
        _tasksHandler = tasksHandler;

        _roomsSystem.RoomsParamsChanged += OnRoomsParamsChanged;
        _guestsSystem.GuestsChanged += OnAvailableGuestsChanged;
    }

    private void OnAvailableGuestsChanged()
    {
        Debug.Log("OnAvailableGuestsChanged");
    }

    public void OnRoomsParamsChanged(Room room)
    {
        Debug.Log("OnRoomsParamsChanged");
        _tasksHandler.CheckAvalibleTasks(room);
    }
}
