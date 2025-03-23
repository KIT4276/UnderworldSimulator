using System;
using System.Collections.Generic;

public class TasksHandler: BaseHandler
{
    public List<Task> CompletedTasks { get; private set; }

    public TasksHandler(TasksData tasksData)
    {
        _all = tasksData.Tasks;

        UpdateAvailable();
        UpdateCompleted();
    }

    private void UpdateCompleted()
    {
        CompletedTasks.Clear();

        foreach (Task task in _all)
        {
            if (task.IsComplete)
            {
                CompletedTasks.Add(task);
            }
        }
    }
}
