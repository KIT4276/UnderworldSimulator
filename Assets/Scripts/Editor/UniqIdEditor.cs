using System.Linq;
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(UniqId))]
public class UniqIdEditor : UnityEditor.Editor
{
    private void OnEnable()
    {
        var uniqId = (UniqId)target;

        if (IsPrefub(uniqId))
            return;

        if (string.IsNullOrEmpty(uniqId.Id))
            Generate(uniqId);
        else
        {
            UniqId[] uniqIds = FindObjectsOfType<UniqId>();

            if (uniqIds.Any(other => other != uniqId && other.Id == uniqId.Id))
                Generate(uniqId);
        }
    }

    private bool IsPrefub(UniqId uniqId) =>
        uniqId.gameObject.scene.rootCount == 0;

    private void Generate(UniqId uniqId)
    {
        uniqId.Id = $"{uniqId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(uniqId);
            EditorSceneManager.MarkSceneDirty(uniqId.gameObject.scene);
        }
    }
}
