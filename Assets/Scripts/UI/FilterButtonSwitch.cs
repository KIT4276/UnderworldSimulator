using UnityEngine;

public class FilterButtonSwitch : MonoBehaviour
{
    [SerializeField] private FilterButton[] _buttons;

    private void Start()
    {
        foreach (var button in _buttons)
        {
            button.GetComponent<ButtonEnterChangeImage>().Activate();
        }
    }

    public void Switch(FilterType filterType)
    {
        foreach (var button in _buttons)
        {
            button.Activate(button.FilterType == filterType);
        }
    }

}

