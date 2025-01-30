using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : UnityEditor.Editor
{
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(Spawner spawner, GizmoType gizmo)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawner.transform.position, 0.5f);
    }
}