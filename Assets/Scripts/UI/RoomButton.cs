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
    private bool _inited;

    private void Awake()
    {
        //_name.gameObject.SetActive(true);   
    }

    public void FillButton(Room room)
    {
       // Debug.Log("FillButton");
        _room = room;
        _name.text = room.ID.ToString();
        _icon.sprite = room.Icon;

        CheckGuest();

        _room.CheckIn += CheckGuest;
        _room.Evicted += CheckGuest;
        _inited = true;
    }

    private void CheckGuest()
    {
        if (_room.Guest != null)
        {
            _gustIcon.gameObject.SetActive(true);
            _gustIcon.sprite = _room.Guest.RoomsChooseIcon;
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
        if (_inited)
        {
            _room.CheckIn -= CheckGuest;
            _room.Evicted -= CheckGuest;
        }
    }
}
