using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomPositioner))]
public class RandomPositionerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RandomPositioner script = (RandomPositioner)target;
        if (GUILayout.Button("Randomize Positions"))
        {
            script.RandomizePositions();
            EditorUtility.SetDirty(script); 
        }
    }
}
