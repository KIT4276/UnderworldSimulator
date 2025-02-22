using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public abstract class BaseMovable : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [Space]
    [SerializeField] protected InputActionReference _moveAction;

    protected PlayerInput _playerInput;
    protected StateMachine _stateMachine;

    protected Vector2 _inputVector2;
    protected bool _canMove;

    [Inject]
    protected void Construct(PlayerInput playerInput, StateMachine stateMachine)
    {
        _playerInput = playerInput;
        _stateMachine = stateMachine;

    }

    public virtual void Immobilize()
    {
        _canMove = false;
        _inputVector2 = Vector2.zero;
    }

    public virtual void Mobilize()
    {
        _canMove = true;
    }

    protected void Update()
    {
        if (_canMove && _inputVector2 != null)
            _inputVector2 = _moveAction.action.ReadValue<Vector2>();
    }
}
