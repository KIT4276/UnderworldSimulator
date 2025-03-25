using TMPro;
using UnityEngine;
using Zenject;

public class RoomMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
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
    [Space]
    [SerializeField] private RoomButton[] _roomButtons;

    [Inject] private StateMachine _machine;
    [Inject] private RoomsSystem _roomsSystem;

    public void Start()
    {
        _roomsSystem.RoomsParamsChanged += UpdateParams;
        _roomsSystem.RoomSelected += UpdateParams;
        _machine.ChangeStateAction += OnChangeState;
        _roomRatingPanel.SetActive(false);
        FillButtons();
    }

    //public void SwitchUpRoom()
    //{
    //    _roomsSystem.SwitchUpRoom();
    //}

    private void FillButtons()
    {
        for (int i = 0; i < _roomsSystem.Rooms.Count; i++)
        {
            //Debug.Log(_roomButtons[i].name);
            _roomButtons[i].FillButton(_roomsSystem.Rooms[i]);
        }
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is WorkbenchState || state is DecorationState)
        {
            // _roomRatingPanel.SetActive(true);
            //_roomsSystem.SwitchUpRoom();

            //Debug.Log(state);

        }
        else
        {
            _roomRatingPanel.SetActive(false);
        }
    }

    public void GoToCheckInGuest()
    {
        _guestMenu.gameObject.SetActive(true);
        _guestMenu.Open();
        this.gameObject.SetActive(false);
    }

    private void UpdateParams(Room room)
    {
        _roomRatingPanel.SetActive(true);
        _name.text = room.Name;
        _nameOfParameter_1.text = RoomParameterNames.Names[room.SetOfParameters.Parameters[0].ParameterType];
        _parameter_1.text = room.SetOfParameters.Parameters[0].Value.ToString();

        _nameOfParameter_2.text = RoomParameterNames.Names[room.SetOfParameters.Parameters[1].ParameterType];
        _parameter_2.text = room.SetOfParameters.Parameters[1].Value.ToString();

        _nameOfParameter_3.text = RoomParameterNames.Names[room.SetOfParameters.Parameters[2].ParameterType];
        _parameter_3.text = room.SetOfParameters.Parameters[2].Value.ToString();
    }

    private void OnDestroy()
    {
        _roomsSystem.RoomsParamsChanged -= UpdateParams;
        _roomsSystem.RoomSelected -= UpdateParams;
        _machine.ChangeStateAction -= OnChangeState;
    }
}
