using DG.Tweening;
using UnityEngine;

public class DOTweenCleaner : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        // Kill all active tweens
        DOTween.KillAll();

        // Completely clear DOTween's internal memory and remove references
        DOTween.Clear(true);

        // Clear any cached tweens that might still be stored
        DOTween.ClearCachedTweens();

        // Find and destroy the hidden DOTween manager object if it still exists
        GameObject dotween = GameObject.Find("[DOTween]");
        if (dotween != null)
        {
            Destroy(dotween);
        }
    }
}
