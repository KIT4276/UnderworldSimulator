using UnityEngine;

public class SwapUIITabs : MonoBehaviour
{
    [SerializeField] private RectTransform _roomTab;
    [SerializeField] private RectTransform _tasksTab;

    public void SwapToRoom()
    {
        AudioManager.Instance.Play(SoundEnum.General_Click);
        _roomTab.SetSiblingIndex(1);
        _tasksTab.SetSiblingIndex(0);
    }

    public void SwapToTasks()
    {
        AudioManager.Instance.Play(SoundEnum.General_Click);
        _tasksTab.SetSiblingIndex(1);
        _roomTab.SetSiblingIndex(0);
    }
}
