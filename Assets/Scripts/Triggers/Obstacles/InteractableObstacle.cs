using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObstacle : MonoBehaviour
{
    [SerializeField] private GameObject _sign;

    private bool _isBroken;
    private bool _isActive;
    private Hero _hero;
    private const string _reactionText = "Реакция персонажа...";

    public event Action Interact;
    public event Action LeftTheArea;

    private void Awake()
    {
        _sign.SetActive(false);
    }

    public void AddIsBroken()
    {
        _isBroken = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out var hero))
        {
            _hero = hero;
            _hero.PlayerInput.onActionTriggered += OnPlayerInputActionTriggered;
            Activate();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DeActivate();
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Interact" && _isActive)
            Interac();
    }

    private void Activate()
    {
        _isActive = true;
        _sign.SetActive(true);
    }

    private void DeActivate()
    {
        _isActive = false;
        _sign.SetActive(false);
        _sign.SetActive(false); 
        _hero.HideReaction();
        LeftTheArea?.Invoke();
    }

    private  void Interac()
    {
        _sign.SetActive(false);

        if (_isBroken)
            Fix();
        else
            _hero.ShowReaction(_reactionText);
    }

    private void Fix()
    {
        //TODO animations
        Interact?.Invoke();
    }
}
