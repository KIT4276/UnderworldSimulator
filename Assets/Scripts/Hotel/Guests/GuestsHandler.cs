using UnityEngine;

public class GuestsHandler : BaseHandler
{
    public GuestsHandler(MilestoneSystem milestoneSystem)
    {
        milestoneSystem.Change += CheckAvalible;
        _milestoneSystem = milestoneSystem;
    }
}
