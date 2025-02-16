using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMove : BaseMovable
{
    [Space]
    [SerializeField] private Rigidbody2D _rigidbody2d;

    public InputAction MoveAction{ get => _moveAction; }


    public float MoveSpeed { get => _moveSpeed; }
    public Vector2 InputVector2 { get => _inputVector2; }

    public void Init()
    {
        _rigidbody2d.gravityScale = 0;
        _rigidbody2d.freezeRotation = true;

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

    /// <summary>
    /// override for tests
    /// </summary>
    public override void Mobilize()
    {
        base.Mobilize();
       // Debug.Log("Hero Mobilize");
    }

    public override void Immobilize()
    {
        base.Immobilize();
       // Debug.Log("Hero Immobilize");
    }
}
