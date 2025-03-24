using System;
using System.Collections.Generic;
using UnityEngine;

public class TasksHandler: BaseHandler
{
    private readonly GuestsSystem _guestsSystem;

    public List<Task> CompletedTasks { get; private set; }

    public TasksHandler(TasksData tasksData, GuestsSystem guestsSystem)
    {
       _guestsSystem = guestsSystem;
        AvailableList = new();
        CompletedTasks = new();

        _all = tasksData.Tasks;

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
        
        foreach (BaseHandledReward task in AvailableList) 
        { 
            if(((Task)task).GuestsType == guest.GuestsType)
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
}
