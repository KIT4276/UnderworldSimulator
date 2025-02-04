using UnityEngine;

public interface IRandomizedPosition
{
    public Transform[] GetTransforms();
    public Vector2 GetMinBounds();
    public Vector2 GetMaxBounds();

}