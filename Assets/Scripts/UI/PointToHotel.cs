using UnityEngine;

public class ArrowPointerToEngineer : MonoBehaviour
{
    public float hideDistance = 100f;        // Hide arrow when close to engineer
    public float screenPadding = 50f;      // Padding from screen edge

    private RectTransform arrowUI;         // This arrow's RectTransform
    private CanvasGroup canvasGroup;       // To control visibility
    private Transform player;
    private Transform engineer;

    void Start()
    {
        arrowUI = GetComponent<RectTransform>();

        // Add CanvasGroup if missing
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        // Find player dynamically if needed
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                return;
        }

        // Find engineer dynamically if needed
        if (engineer == null)
        {
            GameObject engineerObj = GameObject.FindWithTag("Engineer");
            if (engineerObj != null)
                engineer = engineerObj.transform;
            else
                return;
        }

        // Hide arrow if player is close enough
        float distance = Vector2.Distance(player.position, engineer.position);
        if (distance < hideDistance)
        {
            canvasGroup.alpha = 0f;
            return;
        }

        canvasGroup.alpha = 1f;

        // Rotate arrow to point toward engineer
        Vector2 dir = engineer.position - player.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // Convert engineer world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(engineer.position);

        // Clamp to screen edges with padding
        screenPos.x = Mathf.Clamp(screenPos.x, screenPadding, Screen.width - screenPadding);
        screenPos.y = Mathf.Clamp(screenPos.y, screenPadding, Screen.height - screenPadding);

        // Convert screen point to UI local position
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            arrowUI.parent as RectTransform,
            screenPos,
            null, // assuming screen space overlay canvas
            out uiPos
        );

        arrowUI.anchoredPosition = uiPos;
    }
}


/*
// Working solution with fixed arrow:

using UnityEngine;

public class PointToEngineer : MonoBehaviour
{
    private Transform player;
    private Transform engineer;
    private RectTransform arrowUI;
    public float hideDistance = 5f; // Distance threshold to hide the arrow
    private CanvasGroup canvasGroup; // For smooth hiding/showing

    void Start()
    {
        arrowUI = GetComponent<RectTransform>();

        // Optional: use CanvasGroup to control visibility without disabling the object
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        // Try to find the player if it hasn't been found yet
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                return; // Exit early until player exists
        }

        if (engineer == null)
        {
            GameObject engineerObj = GameObject.FindWithTag("Engineer");
            if (engineerObj != null)
                engineer = engineerObj.transform;
        }

        // Direction vector from player to engineer
        Vector3 dir = engineer.position - player.position;
        float distance = dir.magnitude;

        // Hide or show arrow
        if (distance < hideDistance)
        {
            canvasGroup.alpha = 0f; // Fully transparent
        }
        else
        {
            canvasGroup.alpha = 1f; // Visible

            // Rotate arrow to point toward engineer
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
*/