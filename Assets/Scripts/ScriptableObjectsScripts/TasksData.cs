using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "ScriptableObjects/TaskData", order = 4)]

public class TasksData : ScriptableObject
{
    [SerializeField] private ParameterTask[] _parameterTask;
    [SerializeField] private SpecificTask[] _specificTasks;

    //public Task[] Tasks { get => _tasks; }
    //public SpecificTask[] SpecificTasks { get => _specificTasks; }

    public Task[] GetAllTask()
    {
        Task[] all = new Task[_parameterTask.Length + _specificTasks.Length];

        for (int i = 0; i < _parameterTask.Length; i++)
        {
            all[i] = _parameterTask[i];
        }

        for (int i = 0; i < _specificTasks.Length; i++)
        {
            all[_parameterTask.Length + i] = _specificTasks[i];
        }

        //Debug.Log("all " + all.Length);
        return all; 
    }
}
