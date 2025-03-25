using System;
using UnityEngine;

public class RewardsHandler
{
    private MilestoneSystem _milestoneSystem;
    private readonly GuestsStaticData _guestsStaticData;
    private readonly DrawingData _drawingData;
    private readonly TasksData _tasksData;

    public RewardsHandler(MilestoneSystem milestoneSystem, GuestsStaticData guestsStaticData, DrawingData drawingData, TasksData tasksData)
    {
        _milestoneSystem = milestoneSystem;
        _guestsStaticData = guestsStaticData;
        _drawingData = drawingData;
        _tasksData = tasksData;

        InitRewards(_guestsStaticData.Guests);
        InitRewards(_drawingData.Drawings);
        InitRewards(_tasksData.Tasks);
    }

    private void InitRewards(BaseHandledReward[] rewards)
    {
        foreach (var reward in rewards)
        {
            if (reward.MilestonesIndex < 0)
            {
                reward.MakeAvailable();
            }
        }
    }
}
