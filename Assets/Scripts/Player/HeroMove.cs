using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMove : BaseMovable
{
    [SerializeField] private Rigidbody2D _rigidbody2d;


    public InputAction MoveAction{ get => _moveAction; }
    private const string MoveActionName = "Move";


    public float MoveSpeed { get => _moveSpeed; }

    public void Init()
    {
        _rigidbody2d.gravityScale = 0;
        _rigidbody2d.freezeRotation = true;

        _moveAction = _playerInput.currentActionMap.FindAction(MoveActionName);
        _moveAction.Enable();
        Mobilize();
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody2d.position;

        position = position + _inputVector2 * _moveSpeed * Time.deltaTime;

        _rigidbody2d.MovePosition(position);
    }

    public void ChangeMoveSpeed(float value)
    {
        _moveSpeed = value;
    }
}
