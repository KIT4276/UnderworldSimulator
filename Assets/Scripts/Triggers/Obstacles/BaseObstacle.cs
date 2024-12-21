using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    [SerializeField] protected int _sortingOrderFront = 200;
    [SerializeField] protected int _sortingOrderBehind = 90;

    protected SpriteRenderer _spriteRenderer;
    protected HeroMove _hero;
    protected bool _canFade;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroMove>(out HeroMove hero))
        {
            _hero = hero;
            _canFade = true;
            _spriteRenderer.sortingOrder = _sortingOrderFront;
        }
        else
            _canFade = false;
    }


    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroMove>() != null)
        {
            _spriteRenderer.sortingOrder = _sortingOrderBehind;
            _canFade = true;
        }
        else 
            _canFade = false;
    }
}
