using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _menuAuthors;
    // [SerializeField] private GameObject _panelNewGame;
    [SerializeField] private GameObject _panelExit;
    [SerializeField] private GameObject _panelSettings;
    [Space]
    [SerializeField] private InputActionReference _escapeAction;

    public event Action<bool> OnStarted;

    private bool _isTesting = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Initial") return;

        EscapeAboutTheAuthors();
        //_panelNewGame.SetActive(false);
        _panelExit.SetActive(false);
        _menuAuthors.SetActive(false);
        _panelSettings.SetActive(false);

        _escapeAction.action.started += OnEscape;
        AudioReciever.Instance.StartMenu();
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        AudioReciever.Instance.PlayUISettingsClick();
        // _panelNewGame.SetActive(false);
        _panelExit.SetActive(false);
        _menuAuthors.SetActive(false);
        _panelSettings.SetActive(false);

        _buttons.SetActive(true);
    }

    //public void StartNewGamePressed()
    //{
    //    _buttons.SetActive(false);
    //    _panelNewGame.SetActive(true);
    //    AudioReciever.Instance.PlayUISettingsClick();
    //}

    public void StartNewGame()
    {
        _isTesting = false;
        StartGame();
    }

    public void EscapeStartNewGame()
    {
        // _panelNewGame.SetActive(false);
        _buttons.SetActive(true);
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void StartTests()
    {
        _isTesting = true;
        StartGame();
    }

    public void ContinueGame()
    {
        //TODO load PlayerPrefs

        OnStarted?.Invoke(_isTesting);
    }

    public void OpenSettings()
    {
        _buttons.SetActive(false);
        _panelSettings.SetActive(true);
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void EscapeSettings()
    {
        _panelSettings.SetActive(false);
        _buttons.SetActive(true);
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void ExitPressed()
    {
        _buttons.SetActive(false);
        _panelExit.SetActive(true);
        AudioReciever.Instance.PlayUISettingsClick();
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
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void EscapeAboutTheAuthors()
    {
        _buttons.SetActive(true);
        _menuAuthors.SetActive(false);
    }

    private void StartGame()
    {
        AudioReciever.Instance.PlayUISettingsClick();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ContinueGame();
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name != "Initial") return;

        _escapeAction.action.started -= OnEscape;
    }
}
