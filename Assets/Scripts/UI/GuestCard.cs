using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GuestCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    //[SerializeField] private Image _roomIcon;
    [SerializeField] private TMP_Text _roomName;
    [Space]
    [SerializeField] private GameObject _lock;
    [SerializeField] private RoomMenu _roomMenu;

    [Inject] private RoomsSystem _roomsSystem;

    private Guest _guest;

    public void FillCard(Guest guest)
    {
        _lock.SetActive(false);
        _guest = guest;
        _name.text = guest.Name;
        _icon.sprite = guest.Icon;

        if (guest.Room == null)
        {
            //_roomIcon.gameObject.SetActive(false);
            _roomName.text = string.Empty;
        }
        else
        {
            //_roomIcon.gameObject.SetActive(true);
            _roomName.text = guest.Room.Name;
           // _roomIcon.sprite = guest.Room.Icon;
        }

        //TODO Icon and Name of guest

    }

    public void CheckInGuest()
    {
        //_guest.CheckInGuest(_roomsSystem.SelectedRoom);

        //TODO
        _roomsSystem.TryToCheckInGuest(_guest);

        _roomMenu.gameObject.SetActive(true);
        _roomMenu.BackToRooms();
        //
    }

    public void EvictGuest()
    {
        _guest.EvictGuest();
    }

    public void FillCardEmpty()
    {
        _lock.SetActive(true);
    }
}

//[Serializable]
//public class ParamsCard
//{
//    [SerializeField] private TMP_Text _paramName;
//    [SerializeField] private TMP_Text _paramValue;

//    //TODO Icon??

//    public void FillCard(RoomParameter parameter)
//    {
//        _paramName.text = RoomParameterNames.Names[parameter.ParameterType];
//        _paramValue.text = parameter.Value.ToString();
//    }

//    public void FillCardEmpty()
//    {
//        _paramName.text = string.Empty;
//        _paramValue.text = string.Empty;
//    }
//}
