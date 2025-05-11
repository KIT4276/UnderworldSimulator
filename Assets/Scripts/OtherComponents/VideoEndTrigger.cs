using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using Zenject;

public class VideoEndTrigger : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [Space]
    [SerializeField] private InputActionReference[] _continueAction;

    private StateMachine _stateMachine;
    private IPersistantProgressService _progressService;

    [Inject]
    private void Construct(StateMachine stateMachine, IPersistantProgressService progressService)
    {
        _stateMachine = stateMachine;
        _progressService = progressService;

    }

    void Start()
    {
        _videoPlayer.loopPointReached += OnVideoEnd;
        foreach (var action in _continueAction)
        {
            action.action.started += OnContinue;
        }
    }

    private void OnContinue(InputAction.CallbackContext context)
    {
        Debug.Log("stop");
        
        ////_videoPlayer.Stop();
        Continue();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Continue();
    }

    void Continue()
    {
        _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    private void OnDestroy()
    {
        _videoPlayer.loopPointReached -= OnVideoEnd;
        foreach (var action in _continueAction)
        {
            action.action.started -= OnContinue;
        }
    }
}
