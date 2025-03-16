using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [Space, Tooltip("Guest requirements")]
    //[SerializeField] private TMP_Text _hotelRating;
    [Space]
    [SerializeField] private ParamsCard[] _paramsCards;
    [SerializeField] private GameObject _lock;

    public void FillCard(Guest guest)
    {
        _lock.SetActive(false);

        _name.text = guest.Name;
        _icon.sprite = guest.Icon;
       //_hotelRating.text = guest.HotelRating.ToString();

        int i = 0;
        for (; i < guest.GuestRequirements.Parameters.Length; i++)
        {
            _paramsCards[i].FillCard(guest.GuestRequirements.Parameters[i]);
        }
        if (guest.GuestRequirements.Parameters.Length < _paramsCards.Length)
        {
            for (; i < _paramsCards.Length; i++)
            {
                _paramsCards[i].FillCardEmpty();
            }
        }
    }

    public void FillCardEmpty()
    {
        _lock.SetActive(true);
    }
}

[Serializable]
public class ParamsCard
{
    [SerializeField] private TMP_Text _paramName;
    [SerializeField] private TMP_Text _paramValue;

    //TODO Icon??

    public void FillCard(RoomParameter parameter)
    {
        _paramName.text = RoomParameterNames.Names[parameter.ParameterType];
        _paramValue.text = parameter.Value.ToString();
    }

    public void FillCardEmpty()
    {
        _paramName.text = "";
        _paramValue.text = "";
    }
}
