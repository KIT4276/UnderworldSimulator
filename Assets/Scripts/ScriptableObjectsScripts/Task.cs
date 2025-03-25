using System;
using UnityEngine;

[Serializable]
public class Task : BaseHandledReward
{
    [SerializeField] private GuestsType _guestsType;
    [SerializeField] private RoomParameterType _parameterType;
    [SerializeField] private int _value;

    public GuestsType GuestsType { get => _guestsType; }
    public RoomParameterType ParameterType { get => _parameterType; }
    public int Value { get => _value; }
    public bool IsComplete { get; private set; }

    public void ChangeCompleteness(bool isComplete)
    {
        IsComplete = isComplete;
    }
}