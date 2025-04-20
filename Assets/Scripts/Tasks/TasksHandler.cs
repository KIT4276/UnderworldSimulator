using System;
using System.Linq;
using UnityEngine;

public class TasksHandler : BaseHandler
{
    private readonly GuestsSystem _guestsSystem;
    private RoomsSystem _roomsSystem;
    private ProgressSystem _progressSystem;

    public event Action<Task, Room> UpdateTask;

  //  public event Action<Task> CompleteTask;

    public TasksHandler(TasksData tasksData, GuestsSystem guestsSystem, MilestoneSystem milestoneSystem,
        RoomsSystem roomsSystem, ProgressSystem progressSystem)
    {
        _guestsSystem = guestsSystem;
        _milestoneSystem = milestoneSystem;
        _roomsSystem = roomsSystem;
        _progressSystem = progressSystem;
        AvailableList = new();

        _all = tasksData.GetAllTask();

        _roomsSystem.RoomsParamsChanged += UpdateRooms;
        _roomsSystem.RoomSelected += UpdateRooms;
        milestoneSystem.Change += CheckAvalible;

        CheckAvalible();
    }

    private void CheckAllCopmlete()
    {
        if (AvailableList == null || AvailableList.Count == 0) return;

        foreach (var task in AvailableList.ToList())
        {
            CheckCopmlete((Task)task);
        }
    }

    private void CheckCopmlete(Task task)
    {
       //Debug.Log("CheckCopmlete");
        var guest = _guestsSystem.FindGuestByType(task.GuestsType);
        if (guest.Room == null) return;

        if (task is ParameterTask)
        {
            var currentValue = guest.Room.SetOfParameters.GetParamValueByType(((ParameterTask)task).ParameterType);

            if (((ParameterTask)task).Value > currentValue)
            {
                _progressSystem.RemoveCompletedTask(task);
                UpdateTask?.Invoke(task, guest.Room);
            }
            else /*(((ParameterTask)task).Value <= currentValue)*/
            {
                _progressSystem.AddCompletedTask(task);
                UpdateTask?.Invoke(task, guest.Room);
            }


            //if (((ParameterTask)task).Value <= currentValue)
            //{
            //    _progressSystem.AddCompletedTask(task);
            //}
            //else
            //{
            //    _progressSystem.RemoveCompletedTask(task);
            //}
        }

        else if (task is SpecificTask)
        {
           // Debug.Log(guest.Room.InstalledDecor.Count);
            foreach (var decor in guest.Room.InstalledDecor)
            {
                if (((SpecificTask)task).DecorType != decor.DecorType)
                {
                   // Debug.Log("!=");
                    _progressSystem.RemoveCompletedTask(task);
                    UpdateTask?.Invoke(task, guest.Room);
                }
            }

            foreach (var decor in guest.Room.InstalledDecor)
            {
                if (((SpecificTask)task).DecorType == decor.DecorType)
                {
                   // Debug.Log("==");
                    _progressSystem.AddCompletedTask(task);
                    //CompleteTask?.Invoke(task);
                    UpdateTask?.Invoke(task, guest.Room);
                }
            }



            //    if (((SpecificTask)task).DecorType == decor.DecorType)
            //    {
            //        Debug.Log("==");
            //        _progressSystem.AddCompletedTask(task);
            //        //CompleteTask?.Invoke(task);
            //    }
            //    else
            //    {
            //        Debug.Log("!=");
            //        _progressSystem.RemoveCompletedTask(task);
            //    }
            //}

            
        }
    }


    private void UpdateRooms(Room room)
    {
        if (room == null) return;
        if (room.Guest != null)
        {
            foreach (var task in AvailableList)
            {
                if (((Task)task).GuestsType == room.Guest.Type)
                {
                    UpdateTask?.Invoke(((Task)task), room);
                }
            }
        }

        CheckAllCopmlete();
    }

    public void OnDestroy()
    {
        foreach (var task in _all)
        {
            task.MakeUnavailable();
        }
    }
}
