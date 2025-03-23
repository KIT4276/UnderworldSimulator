
using System.Collections.Generic;

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
    }
}
