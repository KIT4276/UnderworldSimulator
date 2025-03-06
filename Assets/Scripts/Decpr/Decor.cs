using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DecorView), (typeof(DecorRotator)))]
[RequireComponent(typeof(DecorDrag), (typeof(DecorPlacer)))]
public class Decor : MonoBehaviour, BaseItem
{
    [SerializeField] private DecorType _decorType;
    [SerializeField] protected Sprite _icon;
      [SerializeField] protected string _hints;
    [Space]
    [SerializeField] private DecorView _decorView;
    [SerializeField] private DecorDrag _decorDrag;
    [SerializeField] private DecorPlacer _decorPlacer;
    [SerializeField] private DecorRotator _decorRotator;
    [Space]
    [SerializeField] protected InputActionReference _clickAction;
    [SerializeField] protected InputActionReference _cancelAction;
    [SerializeField] protected InputActionReference _rotationAction;
   

    private StateMachine _stateMachine;
    protected DecorData _decorData;
    protected DecorationSystem _decorationSystem;
    private bool _canPlace = true;
    protected bool _isCanDecorate;
    private RotationState _currentRotationState;
    private Vector3 _lastPosition;


    public DecorType DecorType { get => _decorType; }
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
        SpaceDeterminantor spaceDeterminantor, int id, DecorHolder decorHolder, StateMachine stateMachine)
    {
        if (ID == 0)
            ID = id;
        IsInside = true;
        IsDragging = true;
        _decorationSystem = decorationSystem;
        _stateMachine = stateMachine;
        _currentRotationState = RotationState.Front;

        InitComponents(staticData, spaceDeterminantor, decorHolder);

        CheckCamera();
        //_isCanDecorate = true;
        OnStateChange(_stateMachine.ActiveState);

        _clickAction.action.performed += OnClick;
        _rotationAction.action.performed += OnRotate;
        _cancelAction.action.performed += OnCancel;

        _stateMachine.ChangeStateAction += OnStateChange;

        SetDecorLayerRecursively(this.gameObject);
    }

    private void SetDecorLayerRecursively(GameObject obj)
    {
        obj.layer = LayerMask.NameToLayer("Decor");

        foreach (Transform child in obj.transform)
            SetDecorLayerRecursively(child.gameObject);
    }

    private void InitComponents(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder)
    {
        _decorView.Initialize(this, staticData, _currentRotationState);
        _decorDrag.Initialize(this, staticData, spaceDeterminantor);
        _decorPlacer.Initialize(this, spaceDeterminantor, decorHolder);
        _decorRotator.Initialize(this, _currentRotationState);
    }

    private void OnStateChange(IExitableState state)
    {
        if (state is DecorationState || state is WorkbenchState)
        {
            _isCanDecorate = true;
            _canPlace = true;
        }
        else
        {
            _isCanDecorate = false;
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
       // Debug.Log("OnCancel");

        if (!IsDragging) return;

        // Debug.Log("IsDragging");
        if (_stateMachine.ActiveState is WorkbenchState)
        {
            GoToLastPosition();
        }
        else if(_stateMachine.ActiveState is DecorationState)
        {
            _decorationSystem.TryToRemoveDecor(this);
        }
    }

    public void RemoveThisDecor()
    {
        //Debug.Log("RemoveThisDecor");
        _decorPlacer.OnRemoved();
        _decorDrag.OnRemoved();
        _decorRotator.OnRemoved();
        _decorView.OnRemoved();

        IsDragging = false;
        IsInside = false;
    }

    private void FixedUpdate()
    {
        if (!CheckCamera()) return;
    }

    public void SetIsCanDecorate(bool isCanDecorate)
    {
        _isCanDecorate = isCanDecorate;
    }

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
        // Debug.Log(_canPlace);
        //Debug.Log(_isCanDecorate);
        if (!_canPlace || !_isCanDecorate) return;
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
        _lastPosition = transform.position;
        IsDragging = false;
        _decorationSystem.InstanriateDecor(this);
        DecorPlacedAction?.Invoke();
        AllowActions();
    }


    private void GoToLastPosition()
    {
        IsDragging = false;
        transform.position = _lastPosition;
        PlaceObject();
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if (!IsDragging || !_isCanDecorate) return;

        Rotated?.Invoke(_currentRotationState);
    }

    public void SetRotationState(RotationState rotationState)
    {
        _currentRotationState = rotationState;
        EndRotation?.Invoke(_currentRotationState);
    }

    public void SetCurrentDecorCollider(Collider2D collider)
        => CurrentDecorCollider = collider;

    public Sprite GetIcon() => _icon;

    public string GetHint() => _hints;

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
        //IsDragging = false;

        _clickAction.action.performed -= OnClick;
        _cancelAction.action.performed -= OnCancel;
        _rotationAction.action.performed -= OnRotate;
        _stateMachine.ChangeStateAction -= OnStateChange;
    }
}
