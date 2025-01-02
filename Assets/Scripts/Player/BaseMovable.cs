using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseMovable : MonoBehaviour
{
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] protected float _moveSpeed;

    protected Vector2 _inputVector2;
    protected InputAction _moveAction;


    protected bool _canMove;

    public virtual void Immobilize()
       => _canMove = false;

    public virtual void Mobilize()
        => _canMove = true;

    protected void Update()
    {
        if (_canMove && _inputVector2 != null)
            _inputVector2 = _moveAction.ReadValue<Vector2>();
    }
}
