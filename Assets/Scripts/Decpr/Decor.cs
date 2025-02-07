using System;
using UnityEditor.SceneManagement;

//using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(DecorView), (typeof(DecorRotator)))]
[RequireComponent(typeof(DecorDrag), (typeof(DecorPlacer)))]
public class Decor : MonoBehaviour, IInventoryObject
{
    [SerializeField] private DecorView _decorView;
    [SerializeField] private DecorDrag _decorDrag;
    [SerializeField] private DecorPlacer _decorPlacer;
    [SerializeField] private DecorRotator _decorRotator;
    [Space]
    [SerializeField] protected InputActionReference _clickAction;
    [SerializeField] protected InputActionReference _cancelAction;
    [SerializeField] protected InputActionReference _rotationAction;
    [Space]
    [SerializeField] protected Sprite _icon;
    //[Space]
    //[SerializeField] protected GameObject _warningSign;

    protected DecorData _decorData;
    protected DecorationSystem _decorationSystem;
    private bool _canPlace = true;
    protected bool _isOnDecorState;
    private RotationState _currentRotationState;

    public bool IsInside { get; private set; }
    public bool IsDragging { get; private set; }
    public Collider2D CurrentDecorCollider { get; private set; }
    public Camera MainCamera { get; private set; }
    public Collider2D CurrentClickableCollider { get; private set; }
    public int ID { get; private set; }

    public event Action DecorPlacedAction;
    public event Action Clicked;
    public event Action<RotationState> Rotated;
    public event Action<RotationState> EndRotation;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem,
        SpaceDeterminantor spaceDeterminantor, int id)
    {
        if (ID == 0)
            ID = id;
        IsInside = true;
        IsDragging = true;
        _decorationSystem = decorationSystem;
        _currentRotationState = RotationState.Front;

        //_warningSign.SetActive(false);

        _decorView.Initialize(this, staticData, _currentRotationState);
        _decorDrag.Initialize(this, staticData, spaceDeterminantor);
        _decorPlacer.Initialize(this);
        _decorRotator.Initialize(this, _currentRotationState);
        CheckCamera();

        _clickAction.action.performed += OnClick;
        _rotationAction.action.performed += OnRotate;
        _cancelAction.action.performed += OnCancel;
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log(IsDragging + " " + ID);

        if (!IsDragging) return;

        _decorationSystem.TryToRemoveDecor(this);
    }

    public void RemoveThisDecor()
    {
        IsDragging = false;
        IsInside = false;

        _decorDrag.OnRemoved();
        _decorRotator.OnRemoved();
        _decorView.OnRemoved();
    }

    private void FixedUpdate()
    {
        if (!CheckCamera()) return;
    }

    public Sprite GetIcon()
        => _icon;

    public void SetIsOnDecorState(bool isOnDecorState) =>
        _isOnDecorState = isOnDecorState;

    public void SetIsInside(bool isInside)
        => IsInside = isInside;

    public void SetCurrentClickableCollider(Collider2D collider2D) =>
        CurrentClickableCollider = collider2D;

    public void TakeDecorIfCan()
    {
        if (_decorationSystem.ActivateDecorIfCan(this))
            IsDragging = true;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!_canPlace || !_isOnDecorState) return;
        //Debug.Log("OnClick");
        Clicked?.Invoke();
    }

    public void BanActions() =>
        _canPlace = false;

    public void AllowActions() =>
         _canPlace = true;

    public void PlaceObject()
    {
        //Debug.Log("PlaceObject");
        IsDragging = false;
        _decorationSystem.InstanriateDecor(this);
        DecorPlacedAction?.Invoke();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if (!IsDragging || !_isOnDecorState) return;

        Rotated?.Invoke(_currentRotationState);
    }

    public void SetRotationState(RotationState rotationState)
    {
        _currentRotationState = rotationState;
        EndRotation?.Invoke(_currentRotationState);
    }

    public void SetCurrentDecorCollider(Collider2D collider)
        => CurrentDecorCollider = collider;

    private bool CheckCamera()
    {
        if (MainCamera != null)
        {
            return true;
        }
        else
        {
            if (Camera.main == null)
            {
                return false;
            }
            else
            {
                MainCamera = Camera.main;
                return true;
            }
        }
    }
    

    protected void OnDisable()
    {
        IsDragging = false;
        _clickAction.action.performed -= OnClick;
        _cancelAction.action.performed -= OnCancel;
        _rotationAction.action.performed -= OnRotate;
    }
}
