using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSignalHandler : MonoBehaviour
{
    //[SerializeField] private PlayerInput _playerInput;

    //public event Action<Vector2> ButtoneClick;

    //private void Awake()
    //{
    //    _playerInput.onActionTriggered += OnPlayerInputActionTriggered;
    //}

    //private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    //{
    //    InputAction action = context.action;

    //    switch (action.name)
    //    {
    //        case "Move":
    //            Vector2 moveCommand = action.ReadValue<Vector2>();
    //            HandleMoveCommand(moveCommand);
    //            break;

    //    }
    //}

    //private void HandleMoveCommand(Vector2 moveCommand)
    //{
    //    if (moveCommand != Vector2.zero)
    //    {
    //        ButtoneClick?.Invoke(moveCommand);
    //    }
    //}
}
