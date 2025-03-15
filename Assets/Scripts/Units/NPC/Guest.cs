using UnityEngine;

public class Guest : MonoBehaviour
{
    [Tooltip("Guest requirements")]
    [SerializeField] private SetOfRoomParameters _setOfParameters;

    public SetOfRoomParameters GuestRequirements { get => _setOfParameters; }
}
