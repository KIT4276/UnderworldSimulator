
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHandler
{
    protected BaseHandledReward[] _all;
    protected MilestoneSystem _milestoneSystem;

    public List<BaseHandledReward> AvailableList { get; protected set; }

    public void UpdateAvailable()
    {
        AvailableList.Clear();

        foreach (BaseHandledReward revard in _all)
        {
            if (revard.IsAvailable)
            {
                AvailableList.Add(revard);
            }
        }
        //Debug.Log("All" + this+ ": " + _all.Length);
        //Debug.Log("Available" + this + ": " + AvailableList.Count);
    }


    protected  void CheckAvalible()
    {
        foreach (var item in _all)
        {
            if (item.MilestonesIndex == _milestoneSystem.CurrentMilestonesIndex() - 1)
            {
                item.MakeAvailable();
            }
        }
    }
}
