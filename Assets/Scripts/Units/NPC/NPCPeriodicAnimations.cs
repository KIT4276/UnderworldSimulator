using DragonBones;
using System.Collections;
using UnityEngine;


public class NPCPeriodicAnimations : MonoBehaviour
{
    [SerializeField] protected UnityArmatureComponent _armatureComponent;
    [SerializeField] protected float _minTime = 20;
    [SerializeField] protected float _maxTime = 50;
    [SerializeField] protected float _fadeInTime = 0.5f;

    protected string _idle = "stand";
    protected string _move = "idel_animation";

    protected void Start()
    {
        
        StartPeriodicMove();
    }

    protected void StartPeriodicMove()
    {
        StopAllCoroutines();
        StartCoroutine(PeriodicMove());
    }

    protected virtual IEnumerator PeriodicMove()
    {
        yield return new WaitForSeconds(RandomTime());

        _armatureComponent.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        AnimateMove();
    }

    protected virtual void AnimateMove()
    {
        _armatureComponent.animation.FadeIn(_move, _fadeInTime, 1);

    }

    protected virtual void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        if (eventObject.animationState.name == _move)
        {
            
            _armatureComponent.animation.Play(_idle, 0);
            StartPeriodicMove();
        }
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
