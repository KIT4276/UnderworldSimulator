//using System.Collections;
using UnityEngine;

public static class ColliderExtensions
{
    public static Vector2[] GetCorners(this Bounds bounds)
    {
        return new Vector2[]
        {
            new Vector2(bounds.min.x, bounds.min.y),
            new Vector2(bounds.min.x, bounds.max.y),
            new Vector2(bounds.max.x, bounds.min.y),
            new Vector2(bounds.max.x, bounds.max.y)
        };
    }
}
