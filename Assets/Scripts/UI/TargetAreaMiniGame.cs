using UnityEngine;

public class TargetAreaMiniGame : MonoBehaviour
{
    [SerializeField] private RectTransform _targetArea;
    [SerializeField] private RectTransform _backArea;

    public void RandomizeCarriagePosition()
    {
        try
        {
            Vector3[] areaCorners = new Vector3[4];
            _backArea.GetWorldCorners(areaCorners);

            float carriageHalfWidth = _targetArea.rect.width * _targetArea.lossyScale.x * 0.5f;
            float minX = areaCorners[0].x + carriageHalfWidth;
            float maxX = areaCorners[2].x - carriageHalfWidth;

            float randomX = Random.Range(minX, maxX);

            Vector3 currentPosition = _targetArea.position;
            currentPosition.x = randomX;
            _targetArea.position = currentPosition;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error randomizing carriage position: {e.Message}");
        }
    }
}
