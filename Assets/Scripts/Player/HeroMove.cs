using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private PlayerInput _playerInput;


    private InputAction _moveAction;
    private Vector2 _inputVector2;

    private const string MoveActionName = "Move";

    public float MoveSpeed { get => _moveSpeed; }

    public void Init()
    {
        _rigidbody2d.gravityScale = 0;
        _rigidbody2d.freezeRotation = true;

        _moveAction = _playerInput.currentActionMap.FindAction(MoveActionName);
        _moveAction.Enable();
    }


    private void Update()
    {
        if (_inputVector2 != null)
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
