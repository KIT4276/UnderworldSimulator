using System;
using UnityEngine;

[Serializable]
public class Task : BaseHandledReward
{
    [SerializeField] protected string _name;
    [SerializeField] private GuestsType _guestsType;
    [SerializeField] private RoomParameterType _parameterType;
    [SerializeField] private int _value;
    [SerializeField] private int _xp;

    public GuestsType GuestsType { get => _guestsType; }
    public RoomParameterType ParameterType { get => _parameterType; }
    public int Value { get => _value; }
    public bool IsComplete { get; private set; }
    public int XP { get => _xp; }

    public override string Name { get => _name;}

    public void ChangeCompleteness(bool isComplete)
    {
        IsComplete = isComplete;
    }
}