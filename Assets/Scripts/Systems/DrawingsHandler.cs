using System.Collections.Generic;
using UnityEngine;

public class DrawingsHandler : BaseHandler
{
    public DrawingsHandler(DrawingData drawingData)
    {
        AvailableList = new();

        _all = drawingData.Drawings;
        AvailableList = new();

        UpdateAvailable();
        
    }
}
