using DG.Tweening;
using UnityEngine;

public static class TweenManager 
{
    public static Tween _fadeTween;

    // Запускает анимацию прозрачности для SpriteRenderer
    public static void StartFade(SpriteRenderer spriteRenderer, float targetAlpha, float duration)
    {
        // Если уже есть активный твин, уничтожаем его перед созданием нового
        if (_fadeTween != null && _fadeTween.IsActive())
        {
            _fadeTween.Kill();
        }

        // Создаём новый твин
        _fadeTween = spriteRenderer.DOFade(targetAlpha, duration);
    }

    // Останавливает текущую анимацию
    public static void StopFade()
    {
        if (_fadeTween != null && _fadeTween.IsActive())
        {
            _fadeTween.Kill();
            _fadeTween = null;
        }
    }
}
