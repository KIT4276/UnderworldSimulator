
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHandler
{
    protected BaseHandledReward[] _all;
    protected MilestoneSystem _milestoneSystem;

    public List<BaseHandledReward> AvailableList { get; protected set; }
    public BaseHandledReward[] All { get => _all; }

    public event Action AvailableUpdate;

    public void UpdateAvailable()
    {
        AvailableList.Clear();

        foreach (BaseHandledReward revard in _all)
        {
            if (revard.IsAvailable)
            {
                AvailableList.Add(revard);
                AvailableUpdate?.Invoke();
            }
        }
    }


    protected virtual void CheckAvalible()
    {
        //if (this is TasksHandler)
        //{
        //    Debug.Log("CheckAvalible");
        //}
        //Debug.Log(_all.Length);
        foreach (var item in _all)
        {
            if (item.MilestonesIndex <= _milestoneSystem.CurrentMilestonesIndex() - 1)
            {
                item.MakeAvailable();
                UpdateAvailable();
            }
            else
            {
                item.MakeUnavailable();
                UpdateAvailable();
            }
        }
    }
}
