using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonChangeImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _normImage;
    [SerializeField] private Sprite _highlightImage;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _highlightImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _normImage;
    }
}
