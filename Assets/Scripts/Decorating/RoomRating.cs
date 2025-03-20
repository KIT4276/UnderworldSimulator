using TMPro;
using UnityEngine;
using Zenject;

public class RoomRating : MonoBehaviour
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

    [Inject] private SpaceDeterminantor _spaceDeterminantor;

    public void Start()
    {
        OnFindFloor();
        _spaceDeterminantor.Find += OnFindFloor;
    }

    public void GoToCheckInGuest()
    {
        _guestMenu.gameObject.SetActive(true);
        _guestMenu.Open();
        this.gameObject.SetActive(false);
    }

    private void OnFindFloor()
    {
        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            floor.Room.ChangeParameter += ShowParameters;
        }
        ShowParameters(_spaceDeterminantor.FloorMarkers[0].Room);
    }

    private void ShowParameters(Room room)
    {
        
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
        _spaceDeterminantor.Find -= OnFindFloor;
        foreach (var floor in _spaceDeterminantor.FloorMarkers)
        {
            floor.Room.ChangeParameter -= ShowParameters;
        }
    }
}
