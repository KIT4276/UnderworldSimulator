using System;
using System.Collections.Generic;
using UnityEngine;

public class TasksHandler : BaseHandler
{
    private readonly GuestsSystem _guestsSystem;
    private RoomsSystem _roomsSystem;

    public event Action<Task, Room> UpdateTask;

    public TasksHandler(TasksData tasksData, GuestsSystem guestsSystem, MilestoneSystem milestoneSystem, RoomsSystem roomsSystem)
    {
        _guestsSystem = guestsSystem;
        _milestoneSystem = milestoneSystem;
        _roomsSystem = roomsSystem;
        AvailableList = new();

        _all = tasksData.Tasks;

        _roomsSystem.RoomsParamsChanged += UpdateRooms;
        _roomsSystem.RoomSelected += UpdateRooms;
        milestoneSystem.Change += CheckAvalible;

        CheckAvalible();
    }

    private void UpdateRooms(Room room)
    {
        if(room.Guest != null)
        {
            foreach(var task in AvailableList)
            {
                if(((Task)task).GuestsType == room.Guest.Type)
                {
                    UpdateTask?.Invoke(((Task)task), room);
                }
            }
        } 
    }

    public void OnDestroy()
    {
        foreach(var task in _all)
        {
            task.MakeUnavailable();
        }
    }
}
