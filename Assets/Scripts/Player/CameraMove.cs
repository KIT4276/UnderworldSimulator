using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : BaseMovable
{
    [SerializeField] protected InputActionReference _scrollction;
    [Space]
    [SerializeField] private CinemachineCamera _followCMVCamera;
    [SerializeField] private CinemachineCamera _manualCMVCamera;
    [SerializeField] private float _scrollSpeed = 10;

    private float _scrollValue;

    private void Start()
    {
        _followCMVCamera.Priority = 11;
        _manualCMVCamera.Priority = 10;

        _scrollction.action.performed += _x => _scrollValue = _x.action.ReadValue<float>();
    }


    private void FixedUpdate()
    {
        if (_canMove)
        {
            var deltaX = _inputVector2.x;
            var deltaY = _inputVector2.y;
            var deltaZ = _scrollValue * _scrollSpeed;

            if (_inputVector2 != Vector2.zero)
                deltaZ = 0f;

            _manualCMVCamera.transform.position += new Vector3(deltaX, deltaY, deltaZ) * _moveSpeed * Time.deltaTime;
        }

        _scrollValue = 0f;
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
