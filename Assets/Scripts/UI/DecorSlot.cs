using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField]    private TMP_Text _name;
    [SerializeField] private TMP_Text _paramValue;

    public void FillSlot(Decor decor, ParameterData parameterData, RoomParameter roomParameter)
    {
        _icon.gameObject.SetActive(true);
        _icon.sprite = decor.GetIcon();
        _name.text = decor.Name;

        foreach (var param in decor.Parameters.Parameters)
        {
            if(param.ParameterType == roomParameter.ParameterType)
                _paramValue.text = param.Value.ToString();
        }
    }

    public void FillEmpty()
    {
        _icon.gameObject.SetActive(false);
    }
}
