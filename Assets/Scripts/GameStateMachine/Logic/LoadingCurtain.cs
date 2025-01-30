using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _curtain;
    [SerializeField] private float _alpraStep = 0.03f;

    public void Show()
    {
        gameObject.SetActive(true);
        _curtain.alpha = 1;
    }

    public void Hide() =>
        StartCoroutine(DoFadeIn());

    private IEnumerator DoFadeIn()
    {
        while (_curtain.alpha > 0)
        {
            _curtain.alpha -= _alpraStep;
            yield return new WaitForSeconds(_alpraStep);
        }
    }
}
