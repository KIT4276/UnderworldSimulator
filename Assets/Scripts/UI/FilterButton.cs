using UnityEngine;

public class FilterButton : MonoBehaviour
{
    [SerializeField] private GameObject _inactiveImage;
    [SerializeField] private GameObject _activeImage;
    [SerializeField] private FilterType _filterType;

    public FilterType FilterType { get => _filterType; }

    public void Activate(bool isActivate)
    {
        if (isActivate)
        {
            _inactiveImage.SetActive(false);
            _activeImage.SetActive(true);
        }
        else
        {
            _activeImage.SetActive(false);
            _inactiveImage.SetActive(true);
        }
    }
}

