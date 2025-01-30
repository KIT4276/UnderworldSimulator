using System;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public event Action OnStarted;

    private void Start()
    {
        StartNewGame(); // temporary
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ContinueGame();
    }

    public void ContinueGame()
    {
        OnStarted?.Invoke();
    }
}
