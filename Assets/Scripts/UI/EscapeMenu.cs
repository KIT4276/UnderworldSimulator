using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference _escapeAction;
    [SerializeField] private GameObject _panel;

    [Inject] private StateMachine _machine;
    [Inject] private StatesTransitor _statesTransitor;


    private void Start()
    {
        _panel.SetActive(false);
        _statesTransitor.EscapeGame += OnEscape;
    }

    private void OnEscape()
    {
        _panel.SetActive(true);
    }

    public void ReturnToGame()
    {
        _panel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }

    private void OnDestroy()
    {
        _statesTransitor.EscapeGame -= OnEscape;
    }
}
