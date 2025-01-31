using UnityEngine;

public class TestCell : MonoBehaviour
{

    public void CastRay(LayerMask cellsLayer)
    {

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector3.forward, 30, cellsLayer);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log($"Обнаружен объект {hit.collider.gameObject.name}!");
            }
        }
    }

    void OnDrawGizmos()
    {
        // Визуализация луча в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 30);
    }
}
