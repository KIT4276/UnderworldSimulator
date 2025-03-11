using DragonBones;
using System.Collections;
using UnityEngine;


public class NPCPeriodicAnimations : MonoBehaviour
{
    [SerializeField] private UnityArmatureComponent _armatureComponent;
    [SerializeField] private float _minTime = 20;
    [SerializeField] private float _maxTime = 50;
    [SerializeField] private float _fadeInTime = 0.5f;

    private string _idle = "stand";
    private string _move = "idel_animation";


    private void Start()
    {
        StopAllCoroutines();
        StartPeriodicMove();
    }

    private void StartPeriodicMove()
    {
        StartCoroutine(PeriodicMove());
    }

    private IEnumerator PeriodicMove()
    {
        yield return new WaitForSeconds(RandomTime());
        _armatureComponent.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        //Debug.Log("_move ");
        _armatureComponent.animation.FadeIn(_move, _fadeInTime, 1);
    }

    private void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        if (eventObject.animationState.name == _move)
        {
           // Debug.Log("_idle ");
            _armatureComponent.animation.Play(_idle, 0);//.FadeIn(_idle, _fadeInTime/2, 0);
            StartPeriodicMove();
        }
    }

    private float RandomTime()
    {
        var r = Random.Range(_minTime, _maxTime);
        return r;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
