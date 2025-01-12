using UnityEngine;

public class GreedHolder 
{
     private SpaceDeterminantor _spaceDeterminantor;
     private IAssets _assets;

    private GridCell[,] _grid;

    public GridCell[,] Grid {  get => _grid; } 

    public GreedHolder(SpaceDeterminantor spaceDeterminantor, IAssets assets)
    {
        _spaceDeterminantor = spaceDeterminantor;
        _assets = assets;

        OnFound();
    }

    private void OnFound()
    {
        foreach (var floor in _spaceDeterminantor.FloorObjects)
        {
            FillIn(floor);
        }
    }

    private void FillIn(Floor floor)
    {
        int xCells = Mathf.FloorToInt(floor.X_NumberOfSquares);
        int yCells = Mathf.FloorToInt(floor.Y_NumberOfSquares);

        _grid = new GridCell[xCells, yCells];

        float cellSize = floor.GetCellSize(); 
        Vector3 floorPosition = floor.transform.position; 

        float startX = floorPosition.x - (xCells * cellSize) / 2f;
        float startY = floorPosition.y - (yCells * cellSize) / 2f;

        for (int x = 0; x < xCells; x++)
        {
            for (int y = 0; y < yCells; y++)
            {
                float centerX = startX + x * cellSize + cellSize / 2f;
                float centerY = startY + y * cellSize + cellSize / 2f;

                _grid[x, y] = new GridCell(centerX, centerY, false); 
            }
        }

        BuildGrid();
    }

    private void BuildGrid()
    {
        foreach (GridCell cell in _grid)
        {
            GameObject r = _assets.Instantiate(AssetPath.CellPath);
            r.transform.position = new Vector3(cell._centerX, cell._centerY, 0);
        }
    }
}
