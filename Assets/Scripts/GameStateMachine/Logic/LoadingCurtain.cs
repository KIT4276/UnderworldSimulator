using System;
using System.Collections;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _curtain;
    [SerializeField] protected float _alpraStep = 0.03f;

    public virtual void Show()
    {
        gameObject.SetActive(true);
        _curtain.alpha = 1;
    }

    public void Hide() =>
        StartCoroutine(DoFadeIn());

    protected IEnumerator DoFadeIn()
    {
        while (_curtain.alpha > 0)
        {
            _curtain.alpha -= _alpraStep;
            yield return new WaitForSeconds(_alpraStep);
        }
        if (_curtain.alpha <= 0)
        {
            OffGameObject();
        }
    }

    protected virtual void OffGameObject()
    {
        gameObject.SetActive(false);
    }
}
