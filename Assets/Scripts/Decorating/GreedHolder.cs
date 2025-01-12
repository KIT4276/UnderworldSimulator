using System;
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

        _spaceDeterminantor.Found += OnFound;
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
        // Вычисляем количество ячеек по X и Y
        int xCells = Mathf.FloorToInt(floor.X_NumberOfSquares);
        int yCells = Mathf.FloorToInt(floor.Y_NumberOfSquares);

        // Создаем массив ячеек
        _grid = new GridCell[xCells, yCells];

        float cellSize = floor.GetCellSize(); // Получаем размер одной ячейки
        Vector3 floorPosition = floor.transform.position; // Центр пола

        // Вычисляем начальную позицию (левый нижний угол)
        float startX = floorPosition.x - (xCells * cellSize) / 2f;
        float startY = floorPosition.y - (yCells * cellSize) / 2f;

        // Заполняем сетку
        for (int x = 0; x < xCells; x++)
        {
            for (int y = 0; y < yCells; y++)
            {
                float centerX = startX + x * cellSize + cellSize / 2f;
                float centerY = startY + y * cellSize + cellSize / 2f;

                _grid[x, y] = new GridCell(centerX, centerY, false); // Создаем ячейку
            }
        }

        BuildGrid();
    }

    private void BuildGrid()
    {
        foreach (GridCell cell in _grid)
        {
            GameObject r = _assets.Instantiate(AssetPath.CellPath);
            r.transform.position = new Vector3(cell._centerX, cell._centerY,0);
        }
    }
}
