using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickChangeImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _normImage;
    [SerializeField] private Sprite _highlightImage;
    [SerializeField] private Sprite _pressedImage;
    [SerializeField] private float _delay = 1;

    public void PressedButtoneChange()
    {
        StartCoroutine(ChangeRoutine());
    }

    private IEnumerator ChangeRoutine()
    {
        _image.sprite = _pressedImage;
        yield return new WaitForSeconds(_delay);
        _image.sprite = _normImage;
    }
}
