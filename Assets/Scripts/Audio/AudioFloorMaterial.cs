using UnityEngine;

public class AudioFloorMaterial : MonoBehaviour
{
    [SerializeField] private FloorMaterial _floorMaterial;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Hero") AudioReciever.Instance.FloorMaterial = _floorMaterial;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Hero") AudioReciever.Instance.FloorMaterial = FloorMaterial.Leaves;
    }
}

public enum FloorMaterial
{
    Leaves,
    Wood,
    Rocks,
    Dirt
}
