using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : BaseMovable
{
    [SerializeField] protected InputActionReference _scrollAction;
    [Space]
    [SerializeField] private CinemachineCamera _followCMVCamera;
    [SerializeField] private CinemachineCamera _manualCMVCamera;
    [SerializeField] private float _scrollSpeed = 10;

    private float _scrollValue;

    private void Start()
    {
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

    private void OnScrollPerformed(InputAction.CallbackContext context)
    {
        _scrollValue = context.ReadValue<float>();
    }

    private void OnDisable()
    {
        _scrollAction.action.performed -= OnScrollPerformed;
    }
}
