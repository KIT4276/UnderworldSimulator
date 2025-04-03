using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomScrollRect : ScrollRect
{
    private bool isScrollbarDragging = false;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Checking if dragging is initiated via Scrollbar
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<Scrollbar>() != null)
        {
            isScrollbarDragging = true;
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        // Allow dragging only if it is started via Scrollbar
        if (isScrollbarDragging)
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        isScrollbarDragging = false;
        base.OnEndDrag(eventData);
    }

    public override void OnScroll(PointerEventData eventData)
    {
        // Scrolling with the wheel should work as usual.
        base.OnScroll(eventData);
    }
}
