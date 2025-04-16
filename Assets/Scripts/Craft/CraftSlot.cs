using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _draft;
    [SerializeField] private DraftParameterUnit[] _draftParameterUnits;


    private Drawing _drawing;

    public event Action<Drawing> DrawingSelected;

    public void OnDrawingSelected()
    {
        if (_drawing == null) return;

        DrawingSelected?.Invoke(_drawing);
    }

    public void FillDrawingData(Drawing drawing)
    {
        //Debug.Log(drawing.Name);
        
        _drawing = drawing;
        _name.text = drawing.Name;
        _description.text = drawing.Description;
        _draft.sprite = drawing.Draft;


        foreach (var unit in _draftParameterUnits)
        {
            unit.FillEmpty();
        }

        for (int i = 0; i < drawing.Decor.Parameters.Parameters.Length; i++)
        {
            _draftParameterUnits[i].Fill(drawing.Decor.Parameters.Parameters[i]);
        }
    }

    public void FillEmpty()
    {
        _drawing = null;
        _name.text = string.Empty;
        //Debug.Log("FillEmpty");
    }
}
