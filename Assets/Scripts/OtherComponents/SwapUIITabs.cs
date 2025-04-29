using UnityEngine;

public class SwapUIITabs : MonoBehaviour
{
    [SerializeField] private RectTransform _roomTab;  
    [SerializeField] private RectTransform _tasksTab; 

    public void SwapToRoom()
    {
        _roomTab.SetSiblingIndex(1);
        _tasksTab.SetSiblingIndex(0);
    }

    public void SwapToTasks()
    {
        _tasksTab.SetSiblingIndex(1);
        _roomTab.SetSiblingIndex(0);
    }
}
