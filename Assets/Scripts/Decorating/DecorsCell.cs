﻿using UnityEngine;

public class DecorsCell : BaceCell
{
    public DecorsCell(float x, float y, bool isOccupied, IAssets assets, GameObject polygonSplitterObject) 
    {
        this.CenterX = x;
        this.CenterY = y;
        this.IsOccupied = isOccupied;
        this.QuadObject = assets.Instantiate(AssetPath.DecorCellPath);
        QuadObject.transform.parent = polygonSplitterObject.transform;
        QuadObject.transform.position = new Vector3(x, y, 0);
        QuadObject.name = "DecorsCell";
        SpriteRenderer = QuadObject.GetComponent<SpriteRenderer>();

        _startColor = SpriteRenderer.color;
    }


    public void HideCell()
    {
        SpriteRenderer.color = new Color(0,0,0,0);
    }

    public void ShowCell()
    {
        SpriteRenderer.color = _startColor;
    }
}
