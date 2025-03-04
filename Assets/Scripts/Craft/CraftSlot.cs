using System;
using TMPro;
using UnityEngine;

public class CraftSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;

    private Drawing _drawing;

    public event Action<Drawing> DrawingSelected;

    public void OnDrawingSelected()
    {
        if(_drawing == null) return;

        DrawingSelected?.Invoke(_drawing);
    }

    public void FillDrawingData(Drawing drawing)
    {
        _drawing = drawing;
        _nameText.text = drawing.Name;
    }

    public void FillEmpty()
    {
        _drawing = null;
        _nameText.text = "";
    }
}
