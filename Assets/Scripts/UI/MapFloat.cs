using UnityEngine;

public class MapFloat : MonoBehaviour
{
    [Header("Floating Settings")]
    [Tooltip("How fast the movement changes")]
    public float intensity = 1f;

    [Tooltip("Maximum distance from the original position")]
    public float maxDistance = 10f;

    [SerializeField] private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Vector2 velocity;
    private float angle;

    void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        angle = Random.Range(0f, 360f);
    }

    void Update()
    {
        // Update angle for noise movement
        angle += Time.deltaTime * intensity;

        // Calculate offset using Perlin noise
        float offsetX = (Mathf.PerlinNoise(angle, 0f) - 0.5f) * 2f * maxDistance;
        float offsetY = (Mathf.PerlinNoise(0f, angle) - 0.5f) * 2f * maxDistance;

        Vector2 targetPosition = originalPosition + new Vector2(offsetX, offsetY);

        // Smooth movement to target
        rectTransform.anchoredPosition = Vector2.SmoothDamp(
            rectTransform.anchoredPosition,
            targetPosition,
            ref velocity,
            0.3f
        );
    }
}
