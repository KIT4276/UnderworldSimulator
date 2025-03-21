using DragonBones;
using System.Collections;
using UnityEngine;

public class BearPeriodicAnimations : NPCPeriodicAnimations
{
    private const string TakeBook = "idle_animation_start";
    private const string PutBool = "idle_animation_finish";
    private const string IdleWithBook = "stand_with_book";
    private const string Idle = "stand";
    private bool _withBook;

    protected override IEnumerator PeriodicMove()
    {
        yield return new WaitForSeconds(RandomTime());
        ChangePose();
    }

    private void ChangePose()
    {
        _armatureComponent.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        AnimateMove();
    }

    protected override void AnimateMove()
    {
        if (!_withBook)
        {
            _move = TakeBook;
            _idle = IdleWithBook;
            _withBook = true;
        }
        else
        {
            _move = PutBool;
            _idle = Idle;
            _withBook = false;
        }
        _armatureComponent.animation.FadeIn(_move, _fadeInTime, 1);
    }

    protected override void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        if (eventObject.animationState.name == _move)
        {
            _armatureComponent.animation.Play(_idle, 0);
            StartPeriodicMove();
        }
    }
}
