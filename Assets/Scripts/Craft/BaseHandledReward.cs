using System;
using UnityEngine;

public abstract class BaseHandledReward
{
    [SerializeField, Tooltip("At what milestone does it become available. -1 - available on start")] protected int _milestonesIndex;

    public int MilestonesIndex { get => _milestonesIndex; }

    public abstract string Name { get; } 
    public bool IsAvailable { get; private set; }

    public event Action<BaseHandledReward> BecameAvailable;

    public virtual void MakeAvailable()
    {
        IsAvailable = true;
        //if (this is Task)
        //{
        //    Debug.Log("MakeAvailable " + Name);
        //}
        BecameAvailable?.Invoke(this);
    }

    public void MakeUnavailable()
    {
        //if (this is Task)
        //{
        //    Debug.Log("Unavailable " + Name);
        //}
        IsAvailable = false;
    }
}