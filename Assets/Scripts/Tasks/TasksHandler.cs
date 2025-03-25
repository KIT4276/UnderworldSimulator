using System;
using System.Collections.Generic;
using UnityEngine;

public class TasksHandler: BaseHandler
{
    private readonly GuestsSystem _guestsSystem;
    //private readonly MilestoneSystem _milestoneSystem;

    public List<Task> CompletedTasks { get; private set; }

    public TasksHandler(TasksData tasksData, GuestsSystem guestsSystem, MilestoneSystem milestoneSystem)
    {
        _guestsSystem = guestsSystem;
        _milestoneSystem = milestoneSystem;
        AvailableList = new();
        CompletedTasks = new();

        _all = tasksData.Tasks;

        milestoneSystem.Change += CheckAvalible;

        UpdateAvailable();
        UpdateCompleted();
    }

    public void CheckAvalibleTasks(Room room)
    {
        Guest guest = _guestsSystem.FindGuestByRoom(room);
        List<Task> guestsTasks = FindAvailableTaskByGuest(guest);
    }

    private List<Task> FindAvailableTaskByGuest(Guest guest)
    {
        List<Task> guestsTasks = new();
        //Debug.Log(AvailableList.Count);
        foreach (BaseHandledReward task in AvailableList)
        {
            Debug.Log(task);
            if (((Task)task) != null && ((Task)task).GuestsType == guest.Type)
            {
                guestsTasks.Add((Task)task);
            }
        }
        return guestsTasks;
    }

    private void UpdateCompleted()
    {
        CompletedTasks.Clear();

        foreach (BaseHandledReward task in AvailableList)
        {
            if (((Task)task).IsComplete)
            {
                CompletedTasks.Add((Task)task);
                AvailableList.Remove(task);
            }
        }
        //Debug.Log("Completed Tasks: " + CompletedTasks.Count);
    }

    //protected override void CheckAvalible()
    //{
    //    foreach (var item in _all)
    //    {
    //        if (item.MilestonesIndex == _milestoneSystem.CurrentMilestonesIndex() - 1)
    //        {
    //            item.MakeAvailable();
    //        }
    //    }
    //}
}
