using UnityEngine;

[RequireComponent(typeof(InteractableObstacle))]
public class FixableObstacle : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private InteractableObstacle _interactableObstacle;

    private void Start()
    {
        _menu.SetActive(false);
        _interactableObstacle.Interact += OpenFixMenu;

        _interactableObstacle.AddIsBroken();
    }

    private void OpenFixMenu()
    {
        _menu.SetActive(true);
        _interactableObstacle.LeftTheArea += CloseFixMenu;
    }

    private void CloseFixMenu()
    {
        _menu.SetActive(false);
    }
}