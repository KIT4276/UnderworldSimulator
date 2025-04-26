using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HotelTransparency : MonoBehaviour
{
    [SerializeField] private Tilemap[] _tilemaps;
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private float _transparency = 0.2f;
    [SerializeField] private float _transparencyDuration = 1.5f;

    private float _currentAlpha = 1f;
    private Coroutine _transparencyCoroutine;

    public void UnTransparent()
    {
        if (Mathf.Approximately(_currentAlpha, 1f)) return;

        StartTransparencyTransition(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroMove>(out var hero))
        {
            if (Mathf.Approximately(_currentAlpha, _transparency)) return;

            StartTransparencyTransition(_transparency);
        }
    }

    private void StartTransparencyTransition(float targetAlpha)
    {
        // Останавливаем предыдущую корутину, если она была
        if (_transparencyCoroutine != null)
        {
            StopCoroutine(_transparencyCoroutine);
        }

        _transparencyCoroutine = StartCoroutine(FadeTransparency(targetAlpha));
    }

    private IEnumerator FadeTransparency(float targetAlpha)
    {
        float startAlpha = _currentAlpha;
        float elapsed = 0f;

        while (elapsed < _transparencyDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / _transparencyDuration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            ApplyAlpha(newAlpha);
            yield return null;
        }

        ApplyAlpha(targetAlpha);
        _transparencyCoroutine = null;
    }

    private void ApplyAlpha(float alpha)
    {
        _currentAlpha = alpha;
        if (_tilemaps != null && _tilemaps.Length != 0)
        {
            foreach (var tilemap in _tilemaps)
            {
                var color = tilemap.color;
                color.a = alpha;
                tilemap.color = color;
            }
        }

        foreach (var renderer in _spriteRenderers)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }

    private void OnDisable()
    {
        if (_transparencyCoroutine != null)
        {
            StopCoroutine(_transparencyCoroutine);
        }
    }

    //private float _alpha = 1;

    //public void UnTransparent()
    //{
    //    if(_alpha == 1) return;
    //    SetTilemapAlpha(1);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.TryGetComponent<HeroMove>(out var hero))
    //    {
    //        if (_alpha == _transparency) return;
    //        _alpha = _transparency;

    //        SetTilemapAlpha(_alpha);
    //    }
    //}


    //private void SetTilemapAlpha(float a)
    //{
    //    _alpha = a;
    //    Color color = _tilemaps[0].color;

    //    foreach (var tilemap in _tilemaps)
    //    {
    //        color = tilemap.color;
    //        color.a = a;
    //        tilemap.color = color;
    //    }
    //    foreach (var renderer in _spriteRenderers)
    //    {
    //        color = renderer.color;
    //        color.a = a;
    //        renderer.color = color;
    //    }
    //}
}

