using System;
using UnityEngine;

public class TasksHandler : BaseHandler
{
    private readonly GuestsSystem _guestsSystem;
    private RoomsSystem _roomsSystem;
    private ProgressSystem _progressSystem;

    public event Action<Task, Room> UpdateTask;

    public TasksHandler(TasksData tasksData, GuestsSystem guestsSystem, MilestoneSystem milestoneSystem, 
        RoomsSystem roomsSystem, ProgressSystem progressSystem)
    {
        _guestsSystem = guestsSystem;
        _milestoneSystem = milestoneSystem;
        _roomsSystem = roomsSystem;
        _progressSystem = progressSystem;
        AvailableList = new();

        _all = tasksData.Tasks;

        _roomsSystem.RoomsParamsChanged += UpdateRooms;
        _roomsSystem.RoomSelected += UpdateRooms;
        milestoneSystem.Change += CheckAvalible;

        CheckAvalible();
    }

    //protected override void CheckAvalible()
    //{
    //    base.CheckAvalible();
    //    CheckAllCopmlete();
    //}


    private void CheckAllCopmlete()
    {
        if (AvailableList == null || AvailableList.Count == 0) return;

        foreach (var task in AvailableList)
        {
            CheckCopmlete((Task)task);
        }
    }

    private void CheckCopmlete(Task task)
    {
        var guest = _guestsSystem.FindGuestByType(task.GuestsType);
        if (guest.Room == null) return;

        var currentValue = guest.Room.SetOfParameters.GetParamValueByType(task.ParameterType);


        if (/*task.IsAvailable &&*/ task.Value <= currentValue)
        {
            _progressSystem.AddCompletedTask(task);
        }
        else
        {
            _progressSystem.RemoveCompletedTask(task);
        }
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

        CheckAllCopmlete();
    }

    public void OnDestroy()
    {
        foreach(var task in _all)
        {
            task.MakeUnavailable();
        }
    }
}
