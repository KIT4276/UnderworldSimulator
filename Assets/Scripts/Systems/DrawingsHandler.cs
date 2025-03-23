using System.Collections.Generic;

public class DrawingsHandler : BaseHandler
{
    public DrawingsHandler(DrawingData drawingData)
    {
        _all = drawingData.Drawings;
        AvailableList = new();

        UpdateAvailable();
    }
}
