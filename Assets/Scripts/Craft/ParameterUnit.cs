using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ParameterUnit : DraftParameterUnit
{
    [SerializeField] private TMP_Text _name;

    public override void Fill(RoomParameter roomParameter, ParameterData parameterData)
    {
        //base.Fill(roomParameter, parameterData);

        if (_parameterData == null)
        {
            _parameterData = parameterData;
        }

        _icon.gameObject.SetActive(true);
        _icon.sprite = _parameterData.FindParamByType(roomParameter.ParameterType).IconForCraft; //roomParameter.Icon;
        _count.gameObject.SetActive(true);
        _count.text = "+" + roomParameter.Value.ToString();
    

    _name.gameObject.SetActive(true);
        _name.text = _parameterData.FindParamByType(roomParameter.ParameterType).Name; //roomParameter.Name.ToString();
        //Debug.Log(roomParameter._parameterData);
    }

    public override void FillEmpty()
    {
        base.FillEmpty();
        _name.gameObject.SetActive(false);
       // Debug.Log("FillEmpty");
    }
}
