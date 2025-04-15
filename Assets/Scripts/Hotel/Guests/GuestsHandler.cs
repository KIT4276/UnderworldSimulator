using UnityEngine;

public class GuestsHandler : BaseHandler
{
    public GuestsHandler(MilestoneSystem milestoneSystem, GuestsStaticData guestsData)
    {
        AvailableList = new();
        
        _all = guestsData.Guests;


        milestoneSystem.Change += CheckAvalible;
        _milestoneSystem = milestoneSystem;

        UpdateAvailable();
    }
}
