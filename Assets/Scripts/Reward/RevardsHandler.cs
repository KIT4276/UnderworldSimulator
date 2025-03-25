using System;
using UnityEngine;

public class RevardsHandler
{
    private MilestoneSystem _milestoneSystem;
    private readonly GuestsStaticData _guestsStaticData;
    private readonly DrawingData _drawingData;
    private readonly TasksData _tasksData;

    public RevardsHandler(MilestoneSystem milestoneSystem, GuestsStaticData guestsStaticData, DrawingData drawingData, TasksData tasksData)
    {
        _milestoneSystem = milestoneSystem;
        _guestsStaticData = guestsStaticData;
        _drawingData = drawingData;
        _tasksData = tasksData;

        InitGuests();
    }

    private void InitGuests()
    {
        foreach (var guest in _guestsStaticData.Guests)
        {
            if (guest.MilestonesIndex < 0)
            {
                guest.MakeAvailable();
            }
        }
    }
}
