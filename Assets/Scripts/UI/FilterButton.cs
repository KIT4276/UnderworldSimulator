using UnityEngine;

public class FilterButton : MonoBehaviour
{
    [SerializeField] private GameObject _inactiveImage;
    [SerializeField] private GameObject _activeImage;
    [SerializeField] public bool _isActive;

    public bool IsActive { get => _isActive; }

    private void Start()
    {
        if (_isActive)
            Activate();
        else
            Deactivate();
    }

    public void SwitchIsActive()
    {
        if (_isActive)
            Deactivate();
        else
            Activate();
    }


    public void Activate()
    {
        _isActive = true;
        _inactiveImage.SetActive(false);
        _activeImage.SetActive(true);
    }

    public void Deactivate()
    {
        _isActive = false;
        _inactiveImage.SetActive(true);
        _activeImage.SetActive(false);
    }
}

