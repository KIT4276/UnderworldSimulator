using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RoomMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _guestsIcon;
    [SerializeField] private Sprite _emptyGuestImage;
    [SerializeField] private Image _roomIcon;
    [Space]
    [SerializeField] private Image _iconOfParameter_1;
    [SerializeField] private TMP_Text _parameter_1;
    [Space]
    [SerializeField] private Image _iconOfParameter_2;
    [SerializeField] private TMP_Text _parameter_2;
    [Space]
    [SerializeField] private Image _iconOfParameter_3;
    [SerializeField] private TMP_Text _parameter_3;
    [Space]
    [SerializeField] private GuestMenu _guestMenu;
    [SerializeField] private GameObject _roomRatingPanel;
    [SerializeField] private GameObject _bottomRoomsPanel;
    [Space]
    [SerializeField] private RoomButton[] _roomButtons;
    [Space]
    [SerializeField] private DecorInRoom _decorInRoom;
    [Space]
    [SerializeField] private GameObject _allGuests;
    

    private StateMachine _machine;
    private RoomsSystem _roomsSystem;
    private ParameterData _parameterData;

    [Inject]
    private void Construct(StateMachine machine, RoomsSystem roomsSystem, ParameterData parameterData)
    {
        _machine = machine;
        _roomsSystem = roomsSystem;
        _parameterData = parameterData;

        _roomsSystem.RoomsParamsChanged += UpdateParams;
        _roomsSystem.RoomSelected += UpdateParams;
        _machine.ChangeStateAction += OnChangeState;
        _roomsSystem.Inited += FillButtons;
    }

    private void Awake()
    {
        UpdateParams(_roomsSystem.SelectedRoom);
    }

    private void FillButtons()
    {
        for (int i = 0; i < _roomsSystem.Rooms.Count; i++)
        {
            _roomButtons[i].FillButton(_roomsSystem.Rooms[i]);
        }
    }

    private void OnChangeState(IExitableState state  )
    {
        if (state is GameLoopState || state is  CraftState)
        {
            _roomRatingPanel.SetActive(false);
            _bottomRoomsPanel.SetActive(false);
        }
    }

    public void GoToCheckInGuest()
    {
        _guestMenu.Open();
    }

    public void UpdateParams(Room room)
    {
        _roomRatingPanel.SetActive(true);
        _name.text = room.Name;
        _roomIcon.sprite = room.Icon;

        if (room.Guest == null)
        {
            _guestsIcon.sprite = _emptyGuestImage;
        }
        else
        {
            _guestsIcon.sprite = room.Guest.CheckInIconBig;
        }

        _iconOfParameter_1.sprite =_parameterData.FindParamByType(room.SetOfParameters.Parameters[0].ParameterType).RoomMenuIcons;
        _parameter_1.text = room.SetOfParameters.Parameters[0].Value.ToString();

        _iconOfParameter_2.sprite = _parameterData.FindParamByType(room.SetOfParameters.Parameters[1].ParameterType).RoomMenuIcons;
        _parameter_2.text = room.SetOfParameters.Parameters[1].Value.ToString();

        _iconOfParameter_3.sprite = _parameterData.FindParamByType(room.SetOfParameters.Parameters[2].ParameterType).RoomMenuIcons;
        _parameter_3.text = room.SetOfParameters.Parameters[2].Value.ToString();

        Decor[] decors = new Decor[room.InstalledDecor.Count]  ;
        room.InstalledDecor.CopyTo(decors);

        _decorInRoom.Fill(decors, _parameterData);
    }

    private void OnDestroy()
    {
        _roomsSystem.RoomsParamsChanged -= UpdateParams;
        _roomsSystem.RoomSelected -= UpdateParams;
        _machine.ChangeStateAction -= OnChangeState;
        _roomsSystem.Inited -= FillButtons;
    }
}
