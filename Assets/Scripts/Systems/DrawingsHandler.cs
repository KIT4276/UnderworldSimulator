using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawingsHandler : BaseHandler
{
    //private readonly MilestoneSystem _milestoneSystem;

    public DrawingsHandler(DrawingData drawingData, MilestoneSystem milestoneSystem)
    {
        AvailableList = new();

        _all = drawingData.Drawings;
        AvailableList = new();

        _milestoneSystem = milestoneSystem;
        milestoneSystem.Change += CheckAvalible;

        UpdateAvailable();
    }

   
}
