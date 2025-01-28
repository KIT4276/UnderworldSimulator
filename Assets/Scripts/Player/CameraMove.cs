using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : BaseMovable
{
    [SerializeField] protected InputActionReference _scrollAction;
    [Space]
    [SerializeField] private CinemachineCamera _followCMVCamera;
    [SerializeField] private CinemachineCamera _manualCMVCamera;
    [SerializeField] private CinemachinePositionComposer _positionComposer;
    [SerializeField] private float _scrollSpeed = 10;
    [SerializeField] private float _moveTime = 1;
    [Space]
    [SerializeField] private float _maxCameraDistance = -4;
    [SerializeField] private float _minCameraDistance = -50;

    private float _scrollValue;
    private HotelPoint _hotelPoint;

    private void Start()
    {
        _manualCMVCamera.transform.parent = null;
        _scrollAction.action.performed += OnScrollPerformed;

        _followCMVCamera.Priority = 11;
        _manualCMVCamera.Priority = 10;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            var deltaX = _inputVector2.x;
            var deltaY = _inputVector2.y;
            var deltaZ = _scrollValue * _scrollSpeed;

            if (_inputVector2 != Vector2.zero)
            {
                deltaZ = 0f;
            }
            _manualCMVCamera.transform.position += new Vector3(deltaX, deltaY, deltaZ) * _moveSpeed * Time.deltaTime;

            if (Camera.main.transform.position.z >= _maxCameraDistance && _scrollValue >= 0)
            {
                _manualCMVCamera.transform.position =
                    new Vector3(FindCameraPos(_manualCMVCamera).x, FindCameraPos(_manualCMVCamera).y, _maxCameraDistance);


            }
            else if (Camera.main.transform.position.z <= _minCameraDistance && _scrollValue <= 0)
            {
                _manualCMVCamera.transform.position =
                   new Vector3(FindCameraPos(_manualCMVCamera).x, FindCameraPos(_manualCMVCamera).y, _minCameraDistance);
            }

        }
        else
        {
            _positionComposer.CameraDistance -= _scrollValue;

            if (Camera.main.transform.position.z >= _maxCameraDistance && _scrollValue >= 0)
            {
                _positionComposer.CameraDistance = -_maxCameraDistance;
            }
            else if (Camera.main.transform.position.z < _minCameraDistance && _scrollValue <= 0)
            {
                _positionComposer.CameraDistance = -_minCameraDistance;
            }
        }

        _scrollValue = 0f;
    }

    private Vector2 FindCameraPos(CinemachineCamera camera)
    {
        var cameraPosX = camera.transform.position.x;
        var cameraPosY = camera.transform.position.y;
        return new Vector2(cameraPosX, cameraPosY);
    }

    public override void Immobilize()
    {
        base.Immobilize();
        _followCMVCamera.Priority = 11;
    }

    public override void Mobilize()
    {
        base.Mobilize();
        ChekHotelPoint();
        _manualCMVCamera.transform.DOMove(
            new Vector3(_hotelPoint.transform.position.x, _hotelPoint.transform.position.y, transform.position.z),
            _moveTime);
        _followCMVCamera.Priority = 0;
    }

    private void ChekHotelPoint()
    {
        if (_hotelPoint == null)
            _hotelPoint = GameObject.FindAnyObjectByType<HotelPoint>();
    }

    private void OnScrollPerformed(InputAction.CallbackContext context)
    {
        _scrollValue = context.ReadValue<float>();
    }


    private void OnDisable()
    {
        _scrollAction.action.performed -= OnScrollPerformed;
    }
}
