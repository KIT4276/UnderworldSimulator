using UnityEngine;

public class Guest : MonoBehaviour
{
    [Tooltip("Guest requirements")]
    [SerializeField] private SetOfRoomParameters _setOfParameters;
    [SerializeField] private int _hotelRating;
    [Space]
    [SerializeField] private GuestsType _type;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isOpen; // for tests

    public SetOfRoomParameters GuestRequirements { get => _setOfParameters; }
    public int HotelRating { get => _hotelRating; }
    public GuestsType GuestsType { get => _type; }
    public string Name { get => _name; }
    public bool IsOpen { get => _isOpen; }//; private set; }// for tests
    public bool IsChecked { get; private set; }
    public Sprite Icon { get=> _icon; }
}


