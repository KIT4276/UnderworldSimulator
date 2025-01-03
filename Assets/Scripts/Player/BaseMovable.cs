﻿using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseMovable : MonoBehaviour
{
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] protected float _moveSpeed;
    [Space]
    [SerializeField] protected InputActionReference _moveAction;

    protected Vector2 _inputVector2;


    protected bool _canMove;

    public virtual void Immobilize()
    {
        _canMove = false;
        _inputVector2 = Vector2.zero;
    }

    public virtual void Mobilize()
        => _canMove = true;

    protected virtual void Update()
    {
        if (_canMove && _inputVector2 != null)
            _inputVector2 = _moveAction.action.ReadValue<Vector2>();
    }
}
