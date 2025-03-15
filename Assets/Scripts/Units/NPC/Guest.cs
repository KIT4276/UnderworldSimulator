using UnityEngine;

public class Guest : MonoBehaviour
{
    [Tooltip("Guest requirements")]
    [SerializeField] private SetOfRoomParameters _setOfParameters;

    [SerializeField] private GuestsType _type;

    public GuestsType GuestsType { get { return _type; } }   

    public SetOfRoomParameters GuestRequirements { get => _setOfParameters; }
}


