using System;
using UnityEngine;

public abstract class BaseHandledReward
{
    //[SerializeField] protected string _name;
    //[SerializeField, Tooltip("Is it avalible on start")] protected bool _isAvalible;
    [SerializeField, Tooltip("At what milestone does it become available. -1 - available on start")] protected int _milestonesIndex;

    public int MilestonesIndex { get => _milestonesIndex; }

    public abstract string Name { get; } //{ get /*=> _name*/; }
    public bool IsAvailable { get /*=> _isAvalible*/; private set; }

    public event Action<BaseHandledReward> BecameAvailable;

    public void MakeAvailable()
    {
        /*_isAvalible*/
        IsAvailable = true;
        // Debug.Log("MakeAvailable " + Name);
        BecameAvailable?.Invoke(this);
    }
}