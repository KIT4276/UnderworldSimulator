using UnityEngine;
using UnityEngine.UI;

public class ScrollViewMenuRebootable : MonoBehaviour
{
    [SerializeField] protected Scrollbar _scrollbar;

    protected void ResetScroll()
    {
        _scrollbar.value = 1f;
    }
}
