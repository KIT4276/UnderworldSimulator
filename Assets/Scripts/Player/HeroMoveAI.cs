using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HeroMove), typeof(NavMeshAgent))]
public class HeroMoveAI : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    // [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _testPref;
    [SerializeField] private Camera _camera;

    private Vector3 _target;
    private Vector2 _moucePosition;
    private const string ClicActionName = "Choice";

    private void Start()
    {
        //_agent.updateRotation = false;
        //_agent.updateUpAxis = false;
        _playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == ClicActionName && context.action.phase == InputActionPhase.Started)
            DetermPosition();
    }


    private void DetermPosition()
    {
        
    }
}
