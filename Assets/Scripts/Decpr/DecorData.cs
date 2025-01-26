using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DecorData
{
    public PositionOnLevel PositionOnLevel { get; private set; }
    public Decor ThisDecorComponent { get; private set; }

    public List <BaceCell> OccupitedCells { get; private set; }


    public DecorData(Decor decor)
    {
        ThisDecorComponent = decor;

        var posX = ThisDecorComponent.transform.position.x;
        var posY = ThisDecorComponent.transform.position.y;
        var posZ = ThisDecorComponent.transform.position.z;

        PositionOnLevel = new(SceneManager.GetActiveScene().name, new Vector3Data(posX, posY, posZ));

        OccupitedCells = new();

        ThisDecorComponent.OnOccupyCell += AddCell;
        ThisDecorComponent.OnEmptyCell += DeleteCell;
    }

    private void DeleteCell()
    {
        foreach (var cell in OccupitedCells)
            cell.EmptyCell();
    }

    public void AddCell(BaceCell gridCell)
    {
        gridCell.OccupyCell();
        OccupitedCells.Add(gridCell);
    }
}
