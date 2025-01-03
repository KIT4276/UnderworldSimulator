using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class CameraMove : BaseMovable
{
    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private CinemachineCamera _followCMVCamera;
    [SerializeField] private CinemachineCamera _manualCMVCamera;

    private void Start()
    {
        _moveAction = _heroMove.MoveAction;
        _followCMVCamera.Priority = 11;
        _manualCMVCamera.Priority= 10;
    }


    private void FixedUpdate()
    {
        if (_canMove)
        {
            _manualCMVCamera.transform.position += (Vector3)_inputVector2 * _moveSpeed * Time.deltaTime;
        }
    }

    public override void Immobilize()
    {
        base.Immobilize();
        _followCMVCamera.Priority = 11;
    }

    public override void Mobilize()
    {
        base.Mobilize();
        _followCMVCamera.Priority = 0;
    }
}
