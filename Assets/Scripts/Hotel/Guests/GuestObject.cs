using UnityEngine;

public class GuestObject : MonoBehaviour
{
    public Guest Guest {  get; private set; }

    public void Init(Guest guest)
    {
        Guest = guest;
    }
}
