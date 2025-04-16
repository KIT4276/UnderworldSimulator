using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorSlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _paramName_1;
    [SerializeField] private TMP_Text _paramValue_1;
    [SerializeField] private TMP_Text _paramName_2;
    [SerializeField] private TMP_Text _paramValue_2;
    [SerializeField] private TMP_Text _paramName_3;
    [SerializeField] private TMP_Text _paramValue_3;

    public void FillSlot(Decor decor, ParameterData parameterData)
    {
        _image.gameObject.SetActive(true);
        _image.sprite = decor.GetIcon();

        _paramName_1.text = parameterData.FindParamByType(decor.Parameters.Parameters[0].ParameterType).Name;
        _paramValue_1.text = decor.Parameters.Parameters[0].Value.ToString();

        _paramName_2.text = parameterData.FindParamByType(decor.Parameters.Parameters[1].ParameterType).Name;
        _paramValue_2.text = decor.Parameters.Parameters[1].Value.ToString();

        _paramName_3.text = parameterData.FindParamByType(decor.Parameters.Parameters[2].ParameterType).Name;
        _paramValue_3.text = decor.Parameters.Parameters[2].Value.ToString();
    }

    public void FillEmpty()
    {
        _image.gameObject.SetActive(false);

        _paramName_1.text = string.Empty;
        _paramValue_1.text = string.Empty;

        _paramName_2.text = string.Empty;
        _paramValue_2.text = string.Empty;

        _paramName_3.text = string.Empty;
        _paramValue_3.text = string.Empty;
    }
}
