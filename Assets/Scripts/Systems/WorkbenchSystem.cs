using UnityEngine;
using Zenject;

public class WorkbenchSystem : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [Inject]
    public void  construct()
    {
        //Debug.Log("WorkbenchSystem started");
    }

    /// <summary>
    /// Should be called when activating a workbench
    /// </summary>
    public void Activate() 
    {
        _panel.SetActive(true); 
        Debug.Log("WorkbenchSystem Activate");
    }

    /// <summary>
    ///  /// <summary>
    /// Should be called when deactivating a workbench
    /// </summary>
    public void DeActivate()
    {
        _panel.SetActive(false);

    }
}
