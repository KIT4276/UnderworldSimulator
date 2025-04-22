using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour
{
    //[SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _unactiveSprite;
    [SerializeField] private ButtonEnterChangeImage _buttonEnterChangeImage;
    [SerializeField] private ButtonClickChangeImage[] _buttonClickChangeImage;



    public void Activate()
    {
        //_button.interactable = true;
        _image.sprite = _activeSprite;
        _buttonEnterChangeImage.Activate();
        foreach (var button in _buttonClickChangeImage)
            button.Activate();
    }

    public void Deactivate()
    {
       // _button.interactable = false;
        _image.sprite = _unactiveSprite;
        _buttonEnterChangeImage.DeActivate();
        foreach (var button in _buttonClickChangeImage)
            button.DeActivate();
    }
}