using UnityEngine;

public class ArrowToWorkbench : MonoBehaviour
{
    public float hideDistance = 5f;         // Hide arrow when near engineer
    public float orbitRadius = 100f;        // Distance from center of screen (UI units)

    private RectTransform arrowUI;
    private CanvasGroup canvasGroup;
    private Transform player;
    private Transform engineer;

    void Start()
    {
        arrowUI = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    void Update()
    {
        // Get player
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Hero");
            if (playerObj != null) player = playerObj.transform;
            else return;
        }

        // Get engineer
        if (engineer == null)
        {
            GameObject engineerObj = GameObject.Find("Engineer");
            if (engineerObj != null) engineer = engineerObj.transform;
            else return;
        }

        // Hide if close enough
        float distance = Vector2.Distance(player.position, engineer.position);
        //float distance = Vector2.Distance(Camera.main.transform.position, engineer.position);

        if (distance < hideDistance)
        {
            canvasGroup.alpha = 0f;
            return;
        }

        canvasGroup.alpha = 1f;

        // Direction from player to engineer
        Vector2 dir = engineer.position - player.position;
        //Vector2 dir = engineer.position - Camera.main.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Rotate arrow to point toward engineer
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // Place arrow at fixed distance from center
        arrowUI.anchoredPosition = dir.normalized * orbitRadius;
    }
}
