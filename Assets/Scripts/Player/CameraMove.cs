using DG.Tweening;
using UnityEngine;

public class CameraMove : BaseMovable
{
    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private float _returnPositionTime = 1;

    private Vector3 _defaultPosition;

    private void Start()
    {
        _defaultPosition = transform.position;
        _moveAction = _heroMove.MoveAction;

        Debug.Log(_defaultPosition);
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            transform.position += (Vector3)_inputVector2 * _moveSpeed * Time.deltaTime;
        }
    }

    public override void Immobilize()
    {
        base.Immobilize();
        //todo OF cinemachine
        FindGlobalPosition();
    }

    public override void Mobilize()
    {
        base.Mobilize();
        //todo ON cinemachine
        ReturnDefaultPosition();
    }

    public void ReturnDefaultPosition()
    {
        Debug.Log("ReturnDefaultPosition");
        transform.DOMove(_defaultPosition, _returnPositionTime);
    }

    public void FindGlobalPosition()
    {
        _defaultPosition = transform.position;
    }
}
