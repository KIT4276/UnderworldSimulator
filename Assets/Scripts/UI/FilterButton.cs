using TMPro;
using UnityEngine;

public class FilterButton : MonoBehaviour
{
    [SerializeField] private GameObject _inactiveImage;
    [SerializeField] private GameObject _activeImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private FilterType _filterType;
    [SerializeField] private string _name;

    public FilterType FilterType { get => _filterType; }

    public void Activate(bool isActivate)
    {
        if (isActivate)
        {
            _inactiveImage.SetActive(false);
            _activeImage.SetActive(true);
            _nameText.text = _name;
        }
        else
        {
            _activeImage.SetActive(false);
            _inactiveImage.SetActive(true);
           // _nameText.SetActive(false);
        }
    }
}

