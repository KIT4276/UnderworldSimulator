
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHandler
{
    protected BaseHandledReward[] _all;

    public List<BaseHandledReward> AvailableList { get; protected set; }

    public void UpdateAvailable()
    {
        AvailableList.Clear();

        foreach (BaseHandledReward revard in _all)
        {
            if (revard.IsAvalible)
            {
                AvailableList.Add(revard);
            }
        }
        //Debug.Log("All" + this+ ": " + _all.Length);
        //Debug.Log("Available" + this + ": " + AvailableList.Count);
    }
}
