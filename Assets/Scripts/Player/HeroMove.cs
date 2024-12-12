using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _moveSpeed = 4;

    private InputAction _moveAction;

    private Rigidbody2D _rigidbody2d;
    private Vector2 _inputVector2;

    private const string MoveActionName = "Move";

    public float MoveSpeed { get => _moveSpeed; }

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _rigidbody2d.gravityScale = 0;
        _rigidbody2d.freezeRotation = true;

        _moveAction = _playerInput.currentActionMap.FindAction(MoveActionName);
        _moveAction.Enable();
    }

    private void Update()
    {
        _inputVector2 = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
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
