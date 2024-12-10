using UnityEngine;

public  class BaseObstacle : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected int _sortingOrderFront = 200;
    [SerializeField] protected int _sortingOrderBehind = 90;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        _spriteRenderer.sortingOrder = _sortingOrderFront;
    }


    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        _spriteRenderer.sortingOrder = _sortingOrderBehind;
    }

}
