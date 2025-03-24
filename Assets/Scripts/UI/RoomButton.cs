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

    private void Start()
    {
        _system.Inited += OnRoomsSystemInit;
    }

    private void OnRoomsSystemInit()
    {
        foreach (Room room in _system.Rooms)
        {
            Debug.Log("OnRoomsSystemInit"); // сюда не заходит!
            FillButton(room);
        }
    }

    private void FillButton(Room room)
    {
        _name.text = room.ID.ToString();
        _icon.sprite = room.Icon; 

        if(room.Guest != null)
        {
            _gustIcon.gameObject.SetActive(true);
            _gustIcon.sprite = room.Guest.Icon;
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
}
