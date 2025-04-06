using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ParameterSlot : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _paramName;
    [SerializeField] private TMP_Text _paramValue;
    [Space]
    [SerializeField] private GameObject _checkImage;

    private GuestsSystem _guestsSystem;

    [Inject]
    private void Construct(GuestsSystem guestsSystem)
    {
        _guestsSystem = guestsSystem;
    }

    public void FillEmpty()
    {
        _image.gameObject.SetActive(false);
        _checkImage.SetActive(false);

        _paramName.text = string.Empty;
        _paramValue.text = string.Empty;
    }

    public void FillSlot(Task task)
    {
        Debug.Log("FillSlot");
        _image.gameObject.SetActive(false);
        _image.gameObject.SetActive(true);
        _image.sprite = _guestsSystem.FindGuestByType(task.GuestsType).Icon; //decor.GetIcon();

        _paramName.text = RoomParameterNames.Names[task.ParameterType];
        _paramValue.text = task.Value.ToString();
    }

    public void SetDone()
    {
        _checkImage.SetActive(true);
    }
   
}