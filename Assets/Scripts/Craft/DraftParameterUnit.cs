using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DraftParameterUnit : MonoBehaviour
{
    [SerializeField] protected Image _icon;
    [SerializeField] protected TMP_Text _count;

    protected ParameterData _parameterData;

    [Inject]
    protected void Construct(ParameterData parameterData)
    {
        _parameterData = parameterData;
    }

    public virtual void Fill(RoomParameter roomParameter, ParameterData parameterData)
    {
        if (_parameterData == null)
        {
            _parameterData = parameterData;
        }
        
        _icon.gameObject.SetActive(true);
        _icon.sprite = _parameterData.FindParamByType(roomParameter.ParameterType).Icon; //roomParameter.Icon;
        _count.gameObject.SetActive(true);
        _count.text = "+" + roomParameter.Value.ToString();
    }
    public virtual void FillEmpty()
    {
        _icon.gameObject.SetActive(false);
        _count.gameObject.SetActive(false);
    }
}
