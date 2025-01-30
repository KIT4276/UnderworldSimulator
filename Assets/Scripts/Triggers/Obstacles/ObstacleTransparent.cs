using DG.Tweening;
using UnityEngine;

public class ObstacleTransparent : BaseObstacle
{
    [SerializeField] private float _transparencyValue = 0.5f;
    [SerializeField] private float _transparencyTime = 1.0f;
    [SerializeField] protected SpriteRenderer _spriteRenderer;

    private Tween _fadeInAnimation;
    private Tween _fadeOutAnimation;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (_canFade)
        {
            _fadeInAnimation = _spriteRenderer.DOFade(_transparencyValue, _transparencyTime);
        }
    }


    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (_canFade)
        {
            _spriteRenderer.DOFade(1, _transparencyTime);
        }
    }

    private void OnDestroy()
    {
        if(_fadeInAnimation != null)
            _fadeInAnimation.Kill();

        if (_fadeInAnimation != null)
            _fadeOutAnimation.Kill();
    }
}
