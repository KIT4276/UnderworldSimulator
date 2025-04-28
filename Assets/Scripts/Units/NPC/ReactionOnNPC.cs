using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReactionOnNPC : MonoBehaviour
{
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private GameObject _sign;
    [SerializeField] private string _reactionText;

    private HeroReaction _hero;
    protected bool _isActive;

    private void Start()
    {
        _sign.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroReaction>(out var hero))
        {
            _sign.SetActive(true);
            _hero = hero;
            _interact.action.started += OnPlayerInputActionTriggered;
            Activate();

        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroReaction>(out var hero))
        {
            _hero = hero;
            DeActivate();
        }
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        _sign.SetActive(false);

        _hero.ShowReaction(_reactionText);
    }

    public void Activate()
    {
        _isActive = true;
        _sign.SetActive(true);
    }

    protected void DeActivate()
    {
        if (_isActive)
        {
            _sign.SetActive(false);
            _hero.HideReaction();
            _interact.action.started -= OnPlayerInputActionTriggered;
            _isActive = false;
        }
    }
}
