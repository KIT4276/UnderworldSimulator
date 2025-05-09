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
    [SerializeField] private ButtonEnterChangeImage _enterChangeImage;


    private Drawing _drawing;
    private ParameterData _parameterData;

    public event Action<Drawing> DrawingSelected;

    public Drawing Drawing { get => _drawing; }

    public void OnDrawingSelected()
    {
        if (_drawing == null) return;

        DrawingSelected?.Invoke(_drawing);
        AudioReciever.Instance.PlayUIGeneralClick("OnDrawingSelected");
    }

    public void FillDrawingData(Drawing drawing, ParameterData parameterData)
    {
        //Debug.Log(drawing.Name);
        if (_parameterData == null)
        {
            _parameterData = parameterData;
        }

        _draft.gameObject.SetActive(true);

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
            _draftParameterUnits[i].Fill(drawing.Decor.Parameters.Parameters[i], _parameterData);
        }
        _enterChangeImage.Activate();
    }

    public void FillEmpty()
    {
        _drawing = null;
        _name.text = string.Empty;
        _description.text = string.Empty;
        _draft.gameObject.SetActive(false);
        //Debug.Log("FillEmpty");
        _enterChangeImage.DeActivate();

        foreach (var unit in _draftParameterUnits)
        {
            unit.FillEmpty();
        }
    }
}
