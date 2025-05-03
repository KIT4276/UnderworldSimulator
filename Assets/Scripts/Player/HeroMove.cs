using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMove : BaseMovable
{
    [Space]
    [SerializeField] private Rigidbody2D _rigidbody2d;

    public InputAction MoveAction { get => _moveAction; }

    public float MoveSpeed { get => _moveSpeed; }
    public Vector2 InputVector2 { get => _inputVector2; }

    [SerializeField] private float _audioStepDistance;
    private float _audioStepDistanceBase;

    public void Initialize(StateMachine stateMachine)
    {
        _rigidbody2d.gravityScale = 0;
        _rigidbody2d.freezeRotation = true;
        _audioStepDistanceBase = _audioStepDistance;

        _stateMachine = stateMachine;
        _stateMachine.ChangeStateAction += OnChangeState;
        Mobilize();
    }
    protected override void Update()
    {
        base.Update();

        if (_inputVector2 != Vector2.zero)
        {
            _audioStepDistance -= Time.deltaTime;
            if (_audioStepDistance <= 0)
            {
                AudioReciever.Instance.PlayFootstep();
                _audioStepDistance = _audioStepDistanceBase;
            }
        }
        else _audioStepDistance = _audioStepDistanceBase;
    }

    private void OnChangeState(IExitableState state)
    {
        if (state is GameLoopState || state is InventoryState || state is PseudoCraftState)
        {
            Mobilize();
        }
        else
        {
            Immobilize();
        }
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

    private void OnDestroy()
    {
        _stateMachine.ChangeStateAction -= OnChangeState;
    }

    ///// <summary>
    ///// override for tests
    ///// </summary>
    //public override void Mobilize()
    //{
    //    base.Mobilize();
    //   // Debug.Log("Hero Mobilize");
    //}

    //public override void Immobilize()
    //{
    //    base.Immobilize();
    //   // Debug.Log("Hero Immobilize");
    //}
}
