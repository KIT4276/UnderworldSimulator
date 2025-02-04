using UnityEngine;

[RequireComponent (typeof(IRandomizedPosition))]
public class RandomPositioner : MonoBehaviour
{
     private IRandomizedPosition _randomizedPosition;

    [ContextMenu("Randomize Positions")]
    public void RandomizePositions()
    {
        _randomizedPosition = GetComponent<IRandomizedPosition>();


        if (_randomizedPosition.GetTransforms() == null || _randomizedPosition.GetTransforms().Length == 0)
        {
            Debug.LogWarning("No objects assigned to move!");
            return;
        }

        foreach (Transform obj in _randomizedPosition.GetTransforms())
        {
            if (obj != null)
            {
                float randomX = Random.Range(_randomizedPosition.GetMinBounds().x, _randomizedPosition.GetMaxBounds().x);
                float randomY = Random.Range(_randomizedPosition.GetMinBounds().y, _randomizedPosition.GetMaxBounds().y);
                obj.position = new Vector3(randomX, randomY, 0);
            }
        }
    }
}
