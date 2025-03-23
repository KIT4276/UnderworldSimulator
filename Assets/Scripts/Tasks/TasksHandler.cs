using System;
using System.Collections.Generic;
using UnityEngine;

public class TasksHandler: BaseHandler
{
    public List<Task> CompletedTasks { get; private set; }

    public TasksHandler(TasksData tasksData)
    {
        AvailableList = new();
        CompletedTasks = new();

        _all = tasksData.Tasks;

        UpdateAvailable();
        UpdateCompleted();
    }

    private void UpdateCompleted()
    {
        CompletedTasks.Clear();

        foreach (BaseHandledReward task in _all)
        {
            if (((Task)task).IsComplete)
            {
                CompletedTasks.Add((Task)task);
            }
        }
        //Debug.Log("Completed Tasks: " + CompletedTasks.Count);
    }
}
