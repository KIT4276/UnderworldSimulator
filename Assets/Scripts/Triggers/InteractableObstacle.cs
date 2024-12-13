using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableObstacle : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    private bool _isActive;
    private Hero _hero;
    private const string _reactionText = "Реакция персонажа...";

    private void Awake()
    {
        _button.SetActive(false);
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hero>(out var hero))
        {
            _hero = hero;
            _hero.PlayerInput.onActionTriggered += OnPlayerInputActionTriggered;
            Activate();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DeActivate();
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == "Interact" && _isActive)
            Interac();
    }

    private void Activate()
    {
        _isActive = true;
        _button.SetActive(true);
    }

    private void DeActivate()
    {
        _isActive = false;
        _button .SetActive(false);
        //_hero.HideReaction();
    }

    private void Interac()
    {
        _button.SetActive(false);
        _hero.ShowReaction(_reactionText);
    }

}
