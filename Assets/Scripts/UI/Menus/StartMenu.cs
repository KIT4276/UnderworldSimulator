using System;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public event Action OnStarted;

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
