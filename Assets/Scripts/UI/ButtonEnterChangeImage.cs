using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEnterChangeImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _emptyImage; 
    [SerializeField] private Sprite _normImage;
    [SerializeField] private Sprite _highlightImage;

    private bool _isOccupied;

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
        Debug.Log("OnPointerEnter");
        if(_isOccupied)
        _image.sprite = _highlightImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isOccupied)
            _image.sprite = _normImage;
    }
}
