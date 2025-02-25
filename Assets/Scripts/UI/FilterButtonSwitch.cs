using UnityEngine;

public class FilterButtonSwitch : MonoBehaviour
{
    [SerializeField] private FilterButton[] _buttons;

    public void Switch(FilterType filterType)
    {
        foreach(var button in _buttons)
        {
            button.Activate(button.FilterType == filterType);
        }
    }

}

