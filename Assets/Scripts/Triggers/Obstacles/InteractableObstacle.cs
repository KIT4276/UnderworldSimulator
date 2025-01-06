﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObstacle : MonoBehaviour
{
    [SerializeField] protected GameObject _sign;

    private bool _isBroken;
    protected bool _isActive;
    protected HeroReaction _hero;
    private const string _reactionText = "Реакция персонажа...";//store text somewhere else

    public event Action Interact;
    public event Action LeftTheArea;

    protected void Awake()
    {
        _sign.SetActive(false);
    }

    public void AddIsBroken()
    {
        _isBroken = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroReaction>(out var hero))
        {
            _hero = hero;
            _hero.PlayerInput.onActionTriggered += OnPlayerInputActionTriggered;
            Activate();
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        DeActivate();
    }

    protected void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        if (_isActive && context.action.name == "Interact" && context.action.phase == InputActionPhase.Started)
            Interac();
    }

    protected void Activate()
    {
        _isActive = true;
        _sign.SetActive(true);
    }

    protected void DeActivate()
    {
        _isActive = false;
        _sign.SetActive(false);
        _hero.HideReaction();
        LeftTheArea?.Invoke();
    }

    protected virtual void Interac()
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
