using UnityEngine;

public class ClousdBehaviour : MonoBehaviour
{
    [SerializeField] public float fadeSpeed = 1f;  
    private SpriteRenderer _spriteRenderer;
    private Color _spriteColor;
    private bool _isFadingOut = false;
    private bool _isFadingIn = false;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.color;
    }

    void Update()
    {
        if (_isFadingOut && _spriteColor.a > 0)
        {
            _spriteColor.a -= fadeSpeed * Time.deltaTime;
            _spriteRenderer.color = _spriteColor;
        }

        if (_isFadingIn && _spriteColor.a < 1)
        {
            _spriteColor.a += fadeSpeed * Time.deltaTime;
            _spriteRenderer.color = _spriteColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HotelPoint>() != null)
        {
            _isFadingOut = true;
            _isFadingIn = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<HotelPoint>() != null)
        {
            _isFadingIn = true;
            _isFadingOut = false;
        }
    }
}
