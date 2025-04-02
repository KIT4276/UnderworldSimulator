using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoomMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _guestsIcon;
    [SerializeField] private TMP_Text _guest;
    [Space]
    [SerializeField] private TMP_Text _nameOfParameter_1;
    [SerializeField] private TMP_Text _parameter_1;
    [Space]
    [SerializeField] private TMP_Text _nameOfParameter_2;
    [SerializeField] private TMP_Text _parameter_2;
    [Space]
    [SerializeField] private TMP_Text _nameOfParameter_3;
    [SerializeField] private TMP_Text _parameter_3;
    [Space]
    [SerializeField] private GuestMenu _guestMenu;
    [SerializeField] private GameObject _roomRatingPanel;
    [SerializeField] private GameObject _bottomRoomsPanel;
    [Space]
    [SerializeField] private RoomButton[] _roomButtons;
    [Space]
    [SerializeField] private DecorInRoom _decorInRoom;
    

    private StateMachine _machine;
    private RoomsSystem _roomsSystem;

    [Inject]
    private void Construct(StateMachine machine, RoomsSystem roomsSystem)
    {
        _machine = machine;
        _roomsSystem = roomsSystem;
    }

    public void Start()
    {
        _roomsSystem.RoomsParamsChanged += UpdateParams;
        _roomsSystem.RoomSelected += UpdateParams;
        _machine.ChangeStateAction += OnChangeState;
        _roomsSystem.Inited += FillButtons;
    }

    private void FillButtons()
    {
        for (int i = 0; i < _roomsSystem.Rooms.Count; i++)
        {
            _roomButtons[i].FillButton(_roomsSystem.Rooms[i]);
        }
    }

    private void OnChangeState(IExitableState state)
    {

        if (state is GameLoopState)
        {
            _roomRatingPanel.SetActive(false);
            _bottomRoomsPanel.SetActive(false);
        }
    }

    public void GoToCheckInGuest()
    {
        _guestMenu.gameObject.SetActive(true);
        _guestMenu.Open();
        this.gameObject.SetActive(false);
    }

    public void BackToRooms()
    {
        _guestMenu.gameObject.SetActive(false);
    }

    public void UpdateParams(Room room)
    {
        _roomRatingPanel.SetActive(true);
        _name.text = room.Name;

        if (room.Guest == null)
        {
            _guest.text = string.Empty;
            _guestsIcon.gameObject.SetActive(false);
        }
        else
        {
            _guest.text = room.Guest.Name;
            _guestsIcon.gameObject.SetActive(true);
            _guestsIcon.sprite = room.Guest.Icon;
        }

        _nameOfParameter_1.text = RoomParameterNames.Names[room.SetOfParameters.Parameters[0].ParameterType];
        _parameter_1.text = room.SetOfParameters.Parameters[0].Value.ToString();

        _nameOfParameter_2.text = RoomParameterNames.Names[room.SetOfParameters.Parameters[1].ParameterType];
        _parameter_2.text = room.SetOfParameters.Parameters[1].Value.ToString();

        _nameOfParameter_3.text = RoomParameterNames.Names[room.SetOfParameters.Parameters[2].ParameterType];
        _parameter_3.text = room.SetOfParameters.Parameters[2].Value.ToString();

        Decor[] decors = new Decor[room.InstalledDecor.Count]  ;
        room.InstalledDecor.CopyTo(decors);

        _decorInRoom.Fill(decors);
    }

    private void OnDestroy()
    {
        _roomsSystem.RoomsParamsChanged -= UpdateParams;
        _roomsSystem.RoomSelected -= UpdateParams;
        _machine.ChangeStateAction -= OnChangeState;
        _roomsSystem.Inited -= FillButtons;
    }
}
