using DG.Tweening;
using UnityEngine;

public static class TweenManager 
{
    public static Tween _fadeTween;

    // ��������� �������� ������������ ��� SpriteRenderer
    public static void StartFade(SpriteRenderer spriteRenderer, float targetAlpha, float duration)
    {
        // ���� ��� ���� �������� ����, ���������� ��� ����� ��������� ������
        if (_fadeTween != null && _fadeTween.IsActive())
        {
            _fadeTween.Kill();
        }

        // ������ ����� ����
        _fadeTween = spriteRenderer.DOFade(targetAlpha, duration);
    }

    // ������������� ������� ��������
    public static void StopFade()
    {
        if (_fadeTween != null && _fadeTween.IsActive())
        {
            _fadeTween.Kill();
            _fadeTween = null;
        }
    }
}
