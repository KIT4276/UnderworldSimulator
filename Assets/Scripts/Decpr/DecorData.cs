using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecorData
{
    public PositionOnLevel PositionOnLevel { get; private set; }
    public Decor ThisDecorComponent { get; private set; }

    public List <GridCell> OccupitedCells { get; private set; }


    public DecorData(Decor decor)
    {
        Debug.Log("DecorData");


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
        Debug.Log("DeleteCell");
        
        foreach (var cell in OccupitedCells)
            cell.EmptyCell();
    }

    public void AddCell(GridCell gridCell)
    {
        Debug.Log("AddCell");
        gridCell.OccupyCell();
        OccupitedCells.Add(gridCell);
    }
}
