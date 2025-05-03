using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEnterChangeImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _emptyImage;
    [SerializeField] private Sprite _normImage;
    [SerializeField] private Sprite _highlightImage;

    [SerializeField] private bool _isOccupied;

    private void Awake()
    {
        if (_isOccupied)
            _image.sprite = _normImage;
    }

    public void Activate()
    {
        _image.sprite = _normImage;
        _isOccupied = true;
    }

    public void DeActivate()
    {
        _image.sprite = _emptyImage;
        _isOccupied = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isOccupied)
            _image.sprite = _highlightImage;
        AudioReciever.Instance.PlayUIGeneralHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isOccupied)
            _image.sprite = _normImage;
    }

    private void OnDisable()
    {
        if (_isOccupied)
            _image.sprite = _normImage;
    }
}
