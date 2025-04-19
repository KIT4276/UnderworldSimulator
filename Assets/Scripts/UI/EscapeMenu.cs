using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference _escapeAction;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _buttons;

    [Inject] private StateMachine _machine;
    [Inject] private StatesTransitor _statesTransitor;


    private void Start()
    {
        _panel.SetActive(false);
        _statesTransitor.EscapeGame += OnEscape;
    }

    public void OnEscape()
    {
        if (_panel.activeSelf)
        {
            ReturnToGame();
        }
        else
        {
            _panel.SetActive(true);
        }
    }

    public void ReturnToGame()
    {
        _panel.SetActive(false);
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
        _buttons.SetActive(false);
    }

    public void EscapeSettings()
    {

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
