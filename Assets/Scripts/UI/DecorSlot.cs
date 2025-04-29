using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorSlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _background;
    [Space]
    [SerializeField] private Image _paramIcon_1;
    [SerializeField] private TMP_Text _paramValue_1;
    [SerializeField] private Image _paramIcon_2;
    [SerializeField] private TMP_Text _paramValue_2;
    [SerializeField] private Image _paramIcon_3;
    [SerializeField] private TMP_Text _paramValue_3;

    public void FillSlot(Decor decor, ParameterData parameterData)
    {
        _background.SetActive(true);
        _image.gameObject.SetActive(true);
        _image.sprite = decor.GetIcon();

        _paramIcon_1.sprite = parameterData.FindParamByType(decor.Parameters.Parameters[0].ParameterType).Icon;
        _paramValue_1.text = decor.Parameters.Parameters[0].Value.ToString();

        _paramIcon_2.sprite = parameterData.FindParamByType(decor.Parameters.Parameters[1].ParameterType).Icon;
        _paramValue_2.text = decor.Parameters.Parameters[1].Value.ToString();

        _paramIcon_3.sprite = parameterData.FindParamByType(decor.Parameters.Parameters[2].ParameterType).Icon;
        _paramValue_3.text = decor.Parameters.Parameters[2].Value.ToString();
    }

    public void FillEmpty()
    {
        _background.SetActive(false);
    }
}
