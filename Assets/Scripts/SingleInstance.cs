using System.Threading;
using UnityEngine;

public class SingleInstance : MonoBehaviour
{
    private static Mutex mutex;
    private static bool createdNew;

    void Awake()
  {
#if UNITY_EDITOR
       // Debug.Log("Игра запущена в редакторе. Множественные экземпляры разрешены.");
#else
    mutex = new Mutex(true, "UniqueApplicationMutexID", out createdNew);

    if (!createdNew)
    {
        Debug.Log("Приложение уже запущено. Закрытие...");
        Application.Quit();
        return;
    }

    DontDestroyOnLoad(this.gameObject);
#endif
    }

    void OnApplicationQuit()
    {
        if (createdNew && mutex != null)
        {
            mutex.ReleaseMutex();
            mutex.Dispose();
        }
    }
}
