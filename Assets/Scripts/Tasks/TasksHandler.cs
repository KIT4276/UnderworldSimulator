using System;
using System.Collections.Generic;
using UnityEngine;

public class TasksHandler : BaseHandler
{
    private readonly GuestsSystem _guestsSystem;

    public List<Task> CompletedTasks { get; private set; }

    public event Action BecameAvailable;

    public TasksHandler(TasksData tasksData, GuestsSystem guestsSystem, MilestoneSystem milestoneSystem)
    {
        _guestsSystem = guestsSystem;
        _milestoneSystem = milestoneSystem;
        AvailableList = new();
        CompletedTasks = new();

        _all = tasksData.Tasks;

        milestoneSystem.Change += CheckAvalible;

        CheckAvalible();
    }




    //public void CheckAvalibleTasks(Room room)
    //{
    //    Guest guest = _guestsSystem.FindGuestByRoom(room);
    //    if (guest != null)
    //    {
    //        List<Task> guestsTasks = FindAvailableTaskByGuest(guest);
    //    }
    //}

    //private List<Task> FindAvailableTaskByGuest(Guest guest)
    //{
    //    List<Task> guestsTasks = new();
    //    foreach (BaseHandledReward task in AvailableList)
    //    {
    //        Debug.Log(task);
    //        if (((Task)task) != null && ((Task)task).GuestsType == guest.Type)
    //        {
    //            guestsTasks.Add((Task)task);
    //        }
    //    }
    //    return guestsTasks;
    //}

    //private void UpdateCompleted()
    //{
    //    CompletedTasks.Clear();

    //    foreach (BaseHandledReward task in AvailableList)
    //    {
    //        if (((Task)task).IsComplete)
    //        {
    //            CompletedTasks.Add((Task)task);
    //            AvailableList.Remove(task);
    //        }
    //    }
    //}

    public void OnDestroy()
    {
        foreach(var task in _all)
        {
            task.MakeUnavailable();
        }
    }
}
