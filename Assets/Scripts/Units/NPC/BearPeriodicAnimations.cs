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
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _timeBetweenSneezes;
    private float _timeBetweenSneezesBase;

    private string _currentIdleState;

    private void Start()
    {
        _timeBetweenSneezesBase = _timeBetweenSneezes;
    }

    protected override IEnumerator PeriodicMove()
    {
        yield return new WaitForSeconds(RandomTime());
        ChangePose();
    }
    private void Update()
    {
        if (_currentIdleState == IdleWithBook)
        {
            _timeBetweenSneezes -= Time.deltaTime;
            if (_timeBetweenSneezes <= 0)
            {
                if (!AudioManager.Instance.IsPlaying(SoundEnum.Bear_Finish) && !AudioManager.Instance.IsPlaying(SoundEnum.Bear_Start)) AudioManager.Instance.Play(SoundEnum.Bear_Sustain, _audioSource);
                _timeBetweenSneezes = _timeBetweenSneezesBase;
            }
        }
    }

    private void ChangePose()
    {
        _armatureComponent.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
        _armatureComponent.AddDBEventListener(EventObject.FRAME_EVENT, OnFrameEvent);
        AnimateMove();
    }
    protected virtual void OnFrameEvent(string type, EventObject eventObject)
    {
        Debug.Log("Frame event triggered: " + eventObject.name);
    }

    protected override void AnimateMove()
    {
        if (!_withBook)
        {
            AudioManager.Instance.Play(SoundEnum.Bear_Start, _audioSource);
            _move = TakeBook;
            _idle = IdleWithBook;
            _withBook = true;
        }
        else
        {
            AudioManager.Instance.Play(SoundEnum.Bear_Finish, _audioSource);
            _move = PutBool;
            _idle = Idle;
            _withBook = false;
        }
        _armatureComponent.animation.FadeIn(_move, _fadeInTime, 1);
    }

    protected override void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        // Debug.Log(eventObject.animationState.name);

        if (eventObject.animationState.name == TakeBook)
        {
            _currentIdleState = IdleWithBook;
            _timeBetweenSneezes = _timeBetweenSneezesBase;
        }
        else
        {
            _currentIdleState = "";
        }

        if (eventObject.animationState.name == _move)
        {
            _armatureComponent.animation.Play(_idle, 0);
            StartPeriodicMove();
        }
    }
}
