using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private Text _reaction;
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _reactionTime = 3;
    [SerializeField] private PlayerInput _playerInput;

    public PlayerInput PlayerInput { get => _playerInput; }

    private void Start()
    {
        _panel.SetActive(false);
    }


    public void ShowReaction(string text)
    {
        _panel.SetActive(true);
        _reaction.text = text;

        StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(_reactionTime);
        HideReaction();
    }

    public void HideReaction()
    {
        _panel.SetActive(false);
    }
}
