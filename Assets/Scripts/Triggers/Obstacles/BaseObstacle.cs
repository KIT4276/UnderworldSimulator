using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
   
    protected HeroMove _hero;
    protected bool _canFade;

    //private void Start()
    //{
    //    _spriteRenderer = GetComponent<SpriteRenderer>();
    //}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroMove>(out HeroMove hero))
        {
            _hero = hero;
            _canFade = true;
        }
        else
            _canFade = false;
    }


    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<HeroMove>() != null)
        {
            _canFade = true;
        }
        else 
            _canFade = false;
    }
}
