using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoomButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;//or number
    [SerializeField] private Image _icon;
    [SerializeField] private Image _gustIcon;
    

    private Room _room;

    [Inject] private RoomsSystem _system;

    public void FillButton(Room room)
    {
        _room = room;
        _name.text = room.ID.ToString();
        _icon.sprite = room.Icon;

        CheckGuest();

        _room.CheckIn += CheckGuest;
        _room.Evicted += CheckGuest;
    }

    private void CheckGuest()
    {
        if (_room.Guest != null)
        {
            _gustIcon.gameObject.SetActive(true);
            _gustIcon.sprite = _room.Guest.Icon;
        }
        else
        {
            _gustIcon.gameObject.SetActive(false);
        }
    }

    public void RoomSelectedButtonDown()
    {
        _system.OnRoomSelected(_room);
        
    }

    private void OnDestroy()
    {
        _room.CheckIn -= CheckGuest;
    }
}
