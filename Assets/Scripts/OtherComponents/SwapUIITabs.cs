using UnityEngine;

public class SwapUIITabs : MonoBehaviour
{
    [SerializeField] private RectTransform _roomTab;  // Первый UI элемент
    [SerializeField] private RectTransform _tasksTab; // Второй UI элемент

    private void Awake()
    {
        Debug.Log(_roomTab.GetSiblingIndex());
        Debug.Log(_tasksTab.GetSiblingIndex());
    }

    public void SwapToRoom()
    {
        Debug.Log("SwapToRoom");
        
        //// Получаем текущие индексы элементов
        //int firstIndex = _roomTab.GetSiblingIndex();
        //int secondIndex = _tasksTab.GetSiblingIndex();

        // Меняем местами индексы
        _roomTab.SetSiblingIndex(1);
        _tasksTab.SetSiblingIndex(0);
    }

    public void SwapToTasks()
    {
        Debug.Log("SwapToTasks");

        //// Получаем текущие индексы элементов
        //int firstIndex = _roomTab.GetSiblingIndex();
        //int secondIndex = _tasksTab.GetSiblingIndex();

        // Меняем местами индексы
        _tasksTab.SetSiblingIndex(1);
        _roomTab.SetSiblingIndex(0);
    }
}
