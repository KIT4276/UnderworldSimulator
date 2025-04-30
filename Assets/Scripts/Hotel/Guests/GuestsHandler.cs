
public class GuestsHandler : BaseHandler
{
    public GuestsHandler(MilestoneSystem milestoneSystem, GuestsStaticData guestsData)
    {
        AvailableList = new();
        
        _all = guestsData.Guests;


        milestoneSystem.Change += CheckAvalible;

        foreach(var item in _all)
        {
            item.BecameAvailable += Check;
        }

        _milestoneSystem = milestoneSystem;

        UpdateAvailable();
    }

    private void Check(BaseHandledReward reward)
    {
        CheckAvalible();
    }
}
