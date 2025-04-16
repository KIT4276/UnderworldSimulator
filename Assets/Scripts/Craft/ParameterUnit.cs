using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ParameterUnit : DraftParameterUnit
{
    [SerializeField] private TMP_Text _name;

    public override void Fill(RoomParameter roomParameter, ParameterData parameterData)
    {
        base.Fill(roomParameter, parameterData);
        _name.gameObject.SetActive(true);
        _name.text = _parameterData.FindParamByType(roomParameter.ParameterType).Name; //roomParameter.Name.ToString();
        //Debug.Log(roomParameter._parameterData);
    }

    public override void FillEmpty()
    {
        base.FillEmpty();
        _name.gameObject.SetActive(false);
        Debug.Log("FillEmpty");
    }
}
