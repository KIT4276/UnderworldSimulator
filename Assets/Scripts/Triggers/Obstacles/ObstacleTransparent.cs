using DG.Tweening;
using UnityEngine;

public class ObstacleTransparent : BaseObstacle
{
    [SerializeField] private float _transparencyValue = 0.5f;
    [SerializeField] private float _transparencyTime = 1.0f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (CanFade())
        {
            _spriteRenderer.DOFade(_transparencyValue, _transparencyTime);
        }
    }


    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (this.gameObject.activeSelf && _hero != null)
        {
            _spriteRenderer.DOFade(1, _transparencyTime);
        }
    }

    private bool CanFade() =>
        this.gameObject.activeSelf && _hero != null;
}
