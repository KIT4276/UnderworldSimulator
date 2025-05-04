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
    [Space]
    [SerializeField] private Image _backingButtons;
    [SerializeField] private Sprite _seletedSprite;
    [SerializeField] private Sprite _unSeletedSprite;

    [Inject] private RoomsSystem _system;

    private Room _room;
    private bool _inited;

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

        _system.RoomSelected += OnRoomSelected;

        OnRoomSelected(_system.SelectedRoom);
    }

    private void OnRoomSelected(Room room)
    {
        if(room!= null && room == _room)
        {
            _backingButtons.sprite = _seletedSprite;
        }
        else
        {
            _backingButtons.sprite = _unSeletedSprite;
        }
    }

    private void CheckGuest()
    {
        if (_room.Guest != null)
        {
            _gustIcon.gameObject.SetActive(true);
            _gustIcon.sprite = _room.Guest.IconRoomeChoose;
        }
        else
        {
            _gustIcon.gameObject.SetActive(false);
        }
    }

    public void RoomSelectedButtonDown()
    {
        _system.OnRoomSelected(_room);
        AudioReciever.Instance.PlayUIRoomClick();

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
