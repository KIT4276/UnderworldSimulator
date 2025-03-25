using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "ScriptableObjects/TaskData", order = 4)]

public class TasksData : ScriptableObject
{
    [SerializeField] private Task[] _tasks;

    public Task[] Tasks { get => _tasks; }
}
