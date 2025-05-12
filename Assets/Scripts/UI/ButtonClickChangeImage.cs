using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickChangeImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _normImage;
    [SerializeField] private Sprite _pressedImage;
    [SerializeField] private float _delay = 0.25f;
    [Space]
    [SerializeField] private Sprite _activeNormSprite;
    [SerializeField] private Sprite _unactiveNormSprite;
    [SerializeField] private Sprite _activePressedSprite;
    [SerializeField] private Sprite _unactivePressedSprite;
    [SerializeField] private SoundEnum _sound = SoundEnum.None;

    public void PressedButtoneChange()
    {
        StartCoroutine(ChangeRoutine());
        if (_sound != SoundEnum.None) AudioManager.Instance.Play(_sound);
    }

    public void Activate()
    {
        _normImage = _activeNormSprite;
        _pressedImage = _activePressedSprite;
    }
    public void DeActivate()
    {
        _normImage = _unactiveNormSprite;
        _pressedImage = _unactivePressedSprite;
    }

    private IEnumerator ChangeRoutine()
    {
        _image.sprite = _pressedImage;
        yield return new WaitForSeconds(_delay);
        _image.sprite = _normImage;
    }

    public void RestartView()
    {
        _image.sprite = _normImage;
    }

    private void OnDisable()
    {
        _image.sprite = _normImage;
        StopAllCoroutines();
    }

}
