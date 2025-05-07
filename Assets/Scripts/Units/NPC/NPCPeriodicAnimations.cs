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
    protected string _move = "idle_animation";

    private Coroutine _coroutine;

    private void Awake()
    {
        StartPeriodicMove();
       // Debug.Log("Awake");
    }

    protected void StartPeriodicMove()
    {
        if (!gameObject.activeInHierarchy) return;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PeriodicMove());
    }

    protected virtual IEnumerator PeriodicMove()
    {
        yield return new WaitForSeconds(RandomTime());

        _armatureComponent.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        Debug.Log("AnimateMove");

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

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    protected void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}
