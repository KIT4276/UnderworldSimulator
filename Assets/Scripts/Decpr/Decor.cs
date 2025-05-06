using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DecorView), (typeof(DecorRotator)))]
[RequireComponent(typeof(DecorDrag), (typeof(DecorPlacer)))]

[Serializable]
public class Decor : MonoBehaviour, IBaseItem
{
    [SerializeField] protected DecorType _decorType;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _hints;
    [Space]
    [SerializeField] protected DecorView _decorView;
    [SerializeField] protected DecorDrag _decorDrag;
    [SerializeField] protected DecorPlacer _decorPlacer;
    [SerializeField] protected DecorRotator _decorRotator;
    [Space]
    [SerializeField] protected InputActionReference _clickAction;
    [SerializeField] protected InputActionReference _cancelAction;
    [SerializeField] protected InputActionReference _rotationAction;
    [Space, Tooltip("For room rating")]
    [SerializeField] private SetOfRoomParameters _parameters;
    //[SerializeField] private int _parameter_2;
    //[SerializeField] private int _parameter_3;

    public SetOfRoomParameters Parameters { get => _parameters; }
    //public int Parameter_2 { get => _parameter_2; }
    //public int Parameter_3 { get => _parameter_3; }

    public DecorType DecorType { get => _decorType; }
    public bool IsInside { get; private set; }

    public bool IsDragging { get; private set; }
    public Collider2D CurrentDecorCollider { get; private set; }
    public Camera MainCamera { get; private set; }
    public Collider2D CurrentClickableCollider { get; private set; }
    public Collider2D CurrentOccupiedZone { get; private set; }
    public int ID { get; private set; }
    public string Name { get; private set; }
    public bool CanPickUp { get; private set; }

    public event Action DecorPlacedAction;
    public event Action Clicked;
    public event Action<RotationState> Rotated;
    public event Action<RotationState> EndRotation;

    protected StateMachine _stateMachine;
    protected DecorData _decorData;
    protected DecorationSystem _decorationSystem;
    protected bool _canPlace = true;
    protected bool _isCanDecorate;
    protected RotationState _currentRotationState;
    protected Vector3 _lastPosition;

    public void Initialize(PersistantStaticData staticData, DecorationSystem decorationSystem,
        SpaceDeterminantor spaceDeterminantor, int id, DecorHolder decorHolder, StateMachine stateMachine, string name)
    {
        if (ID == 0)
            ID = id;

        IsInside = true;
        IsDragging = true;
        _decorationSystem = decorationSystem;
        _stateMachine = stateMachine;
        _currentRotationState = RotationState.Front;
        Name = name;

        InitComponents(staticData, spaceDeterminantor, decorHolder);

        CheckCamera();
        OnStateChange(_stateMachine.ActiveState);

        _clickAction.action.performed += OnClick;
        _rotationAction.action.performed += OnRotate;
        _cancelAction.action.performed += OnCancel;
        _stateMachine.ChangeStateAction += OnStateChange;

        SetDecorLayerRecursively(this.gameObject);
    }

    public void RemoveThisDecor()
    {
        _decorPlacer.OnRemoved();
        _decorDrag.OnRemoved();
        _decorRotator.OnRemoved();
        _decorView.OnRemoved();

        IsDragging = false;
        IsInside = false;

        AudioReciever.Instance.PlayUIBackClick();
    }


    public void SetIsCanDecorate(bool isCanDecorate)
    {
        _isCanDecorate = isCanDecorate;
    }

    public void SetIsInside(bool isInside)
        => IsInside = isInside;

    public void SetCurrentClickableCollider(Collider2D collider2D) =>
        CurrentClickableCollider = collider2D;

    public virtual bool TakeDecorIfCan()
    {
        if (_decorationSystem.ActivateDecorIfCan(this))
        {
            IsDragging = true;
            return true;
        }
        else
            return false;
    }


    public void BanActions() =>
        _canPlace = false;

    public void AllowActions() =>
         _canPlace = true;

    public void SetCurrentDecorCollider(Collider2D collider)
        => CurrentDecorCollider = collider;

    public Sprite GetIcon() => _icon;

    public string GetHint() => _hints;

    public virtual void PlaceObject()
    {
        _lastPosition = transform.position;
        IsDragging = false;
        _decorationSystem.InstanriateDecor(this);
        DecorPlacedAction?.Invoke();
        AllowActions();
        AudioReciever.Instance.PlayUIFurniturePlace();

        //foreach (var param in _parameters.Parameters)
        //{
        //    Debug.Log(param.ParameterType);
        //    Debug.Log(param.Value);
        //}
    }

    public void SetRotationState(RotationState rotationState)
    {
        _currentRotationState = rotationState;
        EndRotation?.Invoke(_currentRotationState);
    }

    protected void FixedUpdate()
    {
        if (!CheckCamera()) return;
    }

    protected void OnClick(InputAction.CallbackContext context)
    {
            if (!_canPlace || !_isCanDecorate) return;
            Clicked?.Invoke();
            AudioReciever.Instance.PlayUIFurnitureClick(); // TODO: Bug when clicking at any space on the screen in "placing furniture mode" is producing multiple OnClick events,
                                                           //  but I don't even have a furniture in my hand and just clicking at rooms and empty spaces
    }

    protected void GoToLastPosition()
    {
        IsDragging = false;
        transform.position = _lastPosition;
        PlaceObject();
    }

    protected void OnRotate(InputAction.CallbackContext context)
    {
        if (!IsDragging || !_isCanDecorate) return;

        Rotated?.Invoke(_currentRotationState);
        AudioReciever.Instance.PlayUIFurnitureRotate();
    }

    protected bool CheckCamera()
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
        _clickAction.action.performed -= OnClick;
        _cancelAction.action.performed -= OnCancel;
        _rotationAction.action.performed -= OnRotate;
        _stateMachine.ChangeStateAction -= OnStateChange;
    }

    protected void SetDecorLayerRecursively(GameObject obj)
    {
        obj.layer = LayerMask.NameToLayer("Decor");

        foreach (Transform child in obj.transform)
            SetDecorLayerRecursively(child.gameObject);
    }

    protected void InitComponents(PersistantStaticData staticData, SpaceDeterminantor spaceDeterminantor, DecorHolder decorHolder)
    {
        _decorView.Initialize(this, staticData, _currentRotationState);
        _decorDrag.Initialize(this, staticData, spaceDeterminantor);
        _decorPlacer.Initialize(this, spaceDeterminantor, decorHolder);
        _decorRotator.Initialize(this, _currentRotationState);
    }

    protected void OnStateChange(IExitableState state)
    {
        if (state is DecorationState || state is WorkbenchState)
        {
            _isCanDecorate = true;
            _canPlace = true;
        }
        //else if (state is CraftState)
        //{
        //    if (IsDragging)
        //    {
        //        Debug.Log("TryToRemoveDecor");
        //        _decorationSystem.TryToRemoveDecor(this);
        //    }
        //}
        else
        {
            _isCanDecorate = false;
        }
    }

    protected void OnCancel(InputAction.CallbackContext context)
    {
        GetRidOfDecor();
    }

    protected void GetRidOfDecor()
    {
        if (!IsDragging) return;

        if (_stateMachine.ActiveState is WorkbenchState)
        {
            GoToLastPosition();
        }
        else if (_stateMachine.ActiveState is DecorationState)
        {
            _decorationSystem.TryToRemoveDecor(this);
        }
    }

    public void BanOnPick()
    {
        CanPickUp = false;
    }

    public void AllowOnPick()
    {
        CanPickUp = true;
    }
}
