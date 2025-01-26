using System.Collections.Generic;

public class GreedHolder
{
    private readonly GreedPolygonSplitter _floor;
    public List<BaceCell> Grid { get => _floor.Cells;  }

    public GreedHolder(GreedPolygonSplitter floor, IAssets assets, PersistantStaticData persistantStaticData)
    {
        _floor = floor;

        _floor.Initialize(assets, persistantStaticData);
    }
}
