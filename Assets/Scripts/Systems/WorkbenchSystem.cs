using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [Inject]
    public void  construct()
    {
       //todo
    }

    /// <summary>
    /// Should be called when activating a workbench
    /// </summary>
    public void ActivateDecoration() 
    {
        _panel.SetActive(true); 
        Debug.Log("WorkbenchSystem Activate");
    }

    /// <summary>
    ///  /// <summary>
    /// Should be called when deactivating a workbench
    /// </summary>
    public void DeActivateDecoration()
    {
        _panel.SetActive(false);

    }
}
