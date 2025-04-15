using System;
using UnityEngine;
using Zenject;

[Serializable]
public class Task : BaseHandledReward
{
    [SerializeField] protected string _name;
    [SerializeField] protected GuestsType _guestsType;
    
    [SerializeField] protected int _xp;

    public GuestsType GuestsType { get => _guestsType; }
   
    public bool IsComplete { get; private set; }
    public int XP { get => _xp; }

    public override string Name { get => _name; }

    public void ChangeCompleteness(bool isComplete)
    {
        IsComplete = isComplete;
    }
}

[Serializable]
public class ParameterTask : Task
{
    [SerializeField] private RoomParameterType _parameterType;
    [SerializeField] private int _value;

    public RoomParameterType ParameterType { get => _parameterType; }
    public int Value { get => _value; }
    public Parameter Parameter { get; private set; }

    [Inject]
    private void Construect(ParameterData parameterData)
    {
        Parameter = parameterData.FintParamByType(_parameterType);
    }
}

[Serializable]
public class SpecificTask : Task
{
    [SerializeField] private DecorType _decoratorType;

    public DecorType DecorType { get => _decoratorType; }
}