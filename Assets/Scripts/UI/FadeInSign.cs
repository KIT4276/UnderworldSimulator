using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInSign : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 2;
    [SerializeField] private Image _image;

    public void StartFadeIn()
    {
        _image.DOKill();

        ResetAlpha();

        var fader = _image.DOFade(0, _fadeTime);
        fader.OnComplete(() => gameObject.SetActive(false));
    }

    private void ResetAlpha()
    {
        var color = _image.color;
        color.a = 1f;
        _image.color = color;
    }

    private void OnDisable()
    {
        _image.DOKill();
    }
}
