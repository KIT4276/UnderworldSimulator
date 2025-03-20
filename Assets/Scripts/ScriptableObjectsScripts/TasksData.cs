using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "ScriptableObjects/TaskData", order = 4)]

public class TasksData : ScriptableObject
{
    [SerializeField] private TaskParameters[] _tasks;

    public TaskParameters[] Tasks { get => _tasks; }
}

[Serializable]
public class TaskParameters
{
    [SerializeField] private string _name;
    [SerializeField] private GuestsType _guestsType;
    [SerializeField] private RoomParameterType _parameterType;
    [SerializeField] private int _value;

    public string Name { get => _name; }
    public GuestsType GuestsType { get => _guestsType; }
    public RoomParameterType ParameterType { get => _parameterType; }
    public int Value { get => _value; }
}