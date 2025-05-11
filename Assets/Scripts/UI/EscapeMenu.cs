using System;
using UnityEngine;
using Zenject;
using UnityEditor;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _goToMainPanel;
    [SerializeField] private GameObject _exitGamePanel;

    [Inject] private StatesTransitor _statesTransitor;
    [Inject] private StateMachine _stateMachine;
    [Inject] private ISaveLoadService _saveLoadService;


    private void Start()
    {
        _panel.SetActive(false);
        _buttons.SetActive(false);
        _settingsPanel.SetActive(false);
        _goToMainPanel.SetActive(false);
        _exitGamePanel.SetActive(false);
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
            _buttons.SetActive(true);
        }
    }

    public void ReturnToGame()
    {
        _panel.SetActive(false);
        _buttons.SetActive(false);
        _settingsPanel.SetActive(false);

        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
        _buttons.SetActive(false);

        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void EscapeSettings()
    {
        _settingsPanel.SetActive(false);
        _buttons.SetActive(true);
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void GoToMain()
    {
        _goToMainPanel.SetActive(true);
        _buttons.SetActive(false);

        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void EscapeGoToMain()
    {
        _goToMainPanel.SetActive(false);
        _buttons.SetActive(true);

        AudioReciever.Instance.PlayUISettingsClick();
    }


    public void SaveAndGoToBootstrap()
    {
        _saveLoadService.SaveProgress();
        _stateMachine.Enter<BootstrapState>();

    }

    public void OpenExitGameMenu()
    {
        _exitGamePanel.SetActive(true);
        _buttons.SetActive(false);
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void EscapeExit()
    {
        _exitGamePanel.SetActive(false);
        _buttons.SetActive(true);
        AudioReciever.Instance.PlayUISettingsClick();
    }

    public void ExitGame()
    {
        //Debug.Log("ExitGame");
        AudioReciever.Instance.PlayUISettingsClick();
        _saveLoadService.SaveProgress();

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
