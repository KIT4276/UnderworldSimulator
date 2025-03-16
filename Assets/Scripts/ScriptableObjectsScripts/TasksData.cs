using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "ScriptableObjects/TaskData", order = 4)]

public class TasksData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private RoomParameterType _parameterType;
    [SerializeField] private int _value;

    public string Name{get => _name; }
    public RoomParameterType ParameterType { get => _parameterType; }
    public int Value { get => _value; }
}
