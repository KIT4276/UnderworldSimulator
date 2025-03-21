using DragonBones;
using System.Collections;
using UnityEngine;

public class PeriodicAnimated : MonoBehaviour
{
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private const string AnimationName = "s_walk";
    private UnityArmatureComponent _armature;

    public void StartAnimate(UnityArmatureComponent armature)
    {
        _armature = armature;

        StopAllCoroutines();
        StartCoroutine(PeriodicMove());
    }

    protected virtual IEnumerator PeriodicMove()
    {
        yield return new WaitForSeconds(RandomTime());
        _armature.animation.Play(AnimationName, 1);
        StartAnimate(_armature);
    }

    protected float RandomTime()
    {
        var r = UnityEngine.Random.Range(_minTime, _maxTime);
        return r;
    }

    protected void OnDestroy()
    {
        StopAllCoroutines();
    }
}
