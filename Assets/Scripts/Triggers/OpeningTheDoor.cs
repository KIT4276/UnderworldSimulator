using UnityEngine;

public class OpeningTheDoor : MonoBehaviour
{
    [SerializeField] protected GameObject _open;
    [SerializeField] protected GameObject _close;

    protected void Start()
    {
        _close.SetActive(true);
        _open.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroMove>(out var hero))
        {
            _open.SetActive(true);
            _close.SetActive(false);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroMove>(out var hero))
        {
            _close.SetActive(true);
            _open.SetActive(false);
        }
    }
}
