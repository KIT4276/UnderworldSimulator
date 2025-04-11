using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Tests : MonoBehaviour
{
    [SerializeField] private InputActionReference _progressUpAction;

    [Inject] private ProgressSystem _progressSystem;


    private void Start()
    {
        _progressUpAction.action.performed += OnProgressUp;
    }

    private void OnProgressUp(InputAction.CallbackContext context)
    {
        _progressSystem.ChangeProgress(10);

    }

    private void OnDestroy()
    {
        _progressUpAction.action.performed -= OnProgressUp;
    }
}
