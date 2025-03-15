using UnityEngine;

public class GuestsPoint : MonoBehaviour
{
    [SerializeField] private GuestsType _type;

    public GuestsType Type { get => _type; }
}
