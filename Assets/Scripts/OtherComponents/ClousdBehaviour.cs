using DG.Tweening;
using UnityEngine;

public class ClousdBehaviour : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _normCAlpha;
    [SerializeField] private float _meltingSpeed = 2;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normCAlpha = _spriteRenderer.color.a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HotelPoint>() != null)
            _spriteRenderer.DOFade(0, _meltingSpeed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HotelPoint>() != null)
            _spriteRenderer.DOFade(_normCAlpha, _meltingSpeed);
    }
}
