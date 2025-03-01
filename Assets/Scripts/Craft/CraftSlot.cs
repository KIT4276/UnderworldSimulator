using System;
using UnityEngine;

public class CraftSlot : MonoBehaviour
{
    private Drawing _drawing;

    public event Action<Drawing> DrawingSelected;

    public void OnDrawingSelected()
    {
        DrawingSelected?.Invoke(_drawing);
    }
}
