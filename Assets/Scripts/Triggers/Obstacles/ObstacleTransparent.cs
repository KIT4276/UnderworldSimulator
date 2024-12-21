using System.Collections;
using UnityEngine;

public class ObstacleTransparent : BaseObstacle
{
    [SerializeField] private AnimationCurve _curve;
    [Space]
    [SerializeField] private float _transparencyValue = 0.5f;
    private float _transparencyTime = 1.0f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (CanFade())
        {
            StartCoroutine(FadeIn());
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (this.gameObject.activeSelf && _hero != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float t = _transparencyTime;
        var startAlpha = _spriteRenderer.color.a;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            var delta = (1 - _curve.Evaluate(t));
            var a = startAlpha + delta;
            _spriteRenderer.color =
                new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, a);

            if (a >= 1)
                yield break;

            yield return 0;
        }
        yield return null;
    }

    private IEnumerator FadeIn()
    {
        float t = _transparencyTime;
        var startAlpha = _spriteRenderer.color.a;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            var delta = (1- _curve.Evaluate(t));

            float a = startAlpha - delta;

            _spriteRenderer.color =
                new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, a);

            if (a <= _transparencyValue)
                yield break;

            yield return 0;
        }
        yield return null;
    }

    private bool CanFade() =>
        this.gameObject.activeSelf && _hero != null;
}
