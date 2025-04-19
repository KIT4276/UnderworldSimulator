using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _menuAuthors;
    [SerializeField] private GameObject _panelNewGame;
    [SerializeField] private GameObject _panelExit;
    [SerializeField] private GameObject _panelSettings;
    [Space]
    [SerializeField] private InputActionReference _escapeAction;

    public event Action OnStarted;

    private void Start()
    {
        EscapeAboutTheAuthors();
        _panelNewGame.SetActive(false);
        _panelExit.SetActive(false);
        _menuAuthors.SetActive(false);
        _panelSettings.SetActive(false);

        _escapeAction.action.started += OnEscape;
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        _panelNewGame.SetActive(false);
        _panelExit.SetActive(false);
        _menuAuthors.SetActive(false);
        _panelSettings.SetActive(false);

        _buttons.SetActive(true);
    }

    public void StartNewGamePressed()
    {
        _buttons.SetActive(false);
        _panelNewGame.SetActive(true);
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ContinueGame();
    }

    public void EscapeStartNewGame()
    {
        _panelNewGame.SetActive(false);
        _buttons.SetActive(true);
    }

    public void ContinueGame()
    {
        //TODO load PlayerPrefs

        OnStarted?.Invoke();
    }

    public void OpenSettings()
    {
        _buttons.SetActive(false);
        _panelSettings.SetActive(true);
    }

    public void EscapeSettings()
    {
        _panelSettings.SetActive(false);
        _buttons.SetActive(true);
    }

    public void ExitPressed()
    {
        _buttons.SetActive(false);
        _panelExit.SetActive(true);
    }

    public void EscapeExit()
    {
        _panelExit.SetActive(false);
        _buttons.SetActive(true);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }

    public void AboutTheAuthors()
    {
        _buttons.SetActive(false);
        _menuAuthors.SetActive(true);
    }

    public void EscapeAboutTheAuthors()
    {
        _buttons.SetActive(true);
        _menuAuthors.SetActive(false);
    }
}
