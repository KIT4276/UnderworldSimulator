using UnityEngine;

public class TestCollider : MonoBehaviour
{
    [SerializeField] protected int _sortingOrderFront = 200;
    [SerializeField] protected int _sortingOrderBehind = 90;

    protected SpriteRenderer _spriteRenderer;
    protected HeroMove _hero;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected  void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<HeroMove>(out HeroMove hero))
        {
            _hero = hero;
            _spriteRenderer.sortingOrder = _sortingOrderFront;
        }

    }


    protected  void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<HeroMove>() != null)
            _spriteRenderer.sortingOrder = _sortingOrderBehind;
    }
}
