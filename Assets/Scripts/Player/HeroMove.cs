using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Rigidbody2D _rigidbody;
    //[SerializeField] private CharacterController _characterController;
    [Space]
    [SerializeField] private float _moveSpeed = 2;

    private bool _facingRight;
    private Vector2 _moveVector;

    public bool IsMoving { get; private set; }
    public float MoveSpeed { get => _moveSpeed;  }

    public void ChangeMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    private void Awake()
    {
        _playerInput.onActionTriggered += OnPlayerInputActionTriggered;

        //_rigidbody.freezeRotation = true;
        //_rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _facingRight = transform.localScale.x > 0;
    }

    private void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        ToMove();
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        InputAction action = context.action;

        switch (action.name)
        {
            case "Move":

                Vector2 moveCommand = action.ReadValue<Vector2>();
                HandleMoveCommand(moveCommand);

                switch (action.phase)
                {
                    case InputActionPhase.Canceled:
                        _rigidbody.linearVelocity = Vector2.zero;
                        IsMoving = false;
                        break;

                    case InputActionPhase.Performed:
                        IsMoving = true;
                        break;
                }
                break;

        }
    }

    private void HandleMoveCommand(Vector2 moveCommand)
    {
        _moveVector = moveCommand;
        _rigidbody.AddForce(_moveVector * _moveSpeed);

    }

    private void Flip()
    {
        if (_moveVector.x != 0)
        {
            if (_moveVector.x > 0 && !_facingRight)
            {
                _facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (_moveVector.x < 0 && _facingRight)
            {
                _facingRight = false;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private void ToMove()
    {
        //_rigidbody.AddForce(_moveVector * _moveSpeed);
        //_rigidbody.linearVelocity = Vector2.zero;
        if (!IsMoving)
            _rigidbody.linearVelocity = Vector2.zero;

    }
}
