using DragonBones;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TestAnimation : MonoBehaviour
{
    [SerializeField] private UnityArmatureComponent _armatureComponent;

    [Inject] protected PlayerInput _playerInput;

    private void Start()
    {
        _armatureComponent.animation.Stop();
        _playerInput.actions.FindAction("Cleek").performed += OnCleek;
    }

    private void OnCleek(InputAction.CallbackContext context)
    {
        _armatureComponent.animation.FadeIn("animtion0", 0.5f, 1);
    }

    private void OnDisable()
    {
        if (_playerInput != null)
            _playerInput.actions.FindAction("Cleek").performed -= OnCleek;
    }
}
