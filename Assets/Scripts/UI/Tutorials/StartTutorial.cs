using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using System.Collections;

public class StartTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialBox;
    [SerializeField] private InputActionAsset inputActions; // Drag your .inputactions asset here
    private InputAction skipAction;


    private Animator animtut;
    private StateMachine stateMachine;
    private InputAction advanceAction;
    private bool tutorialStarted = false;

    [Inject]
    public void Construct(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachine.ChangeStateAction += OnStateChange;
    }

    private void Awake()
    {
        if (stateMachine.IsTests) return;

        animtut = GetComponent<Animator>();
        animtut.enabled = false;

        // Create input action to listen to Space bar
        advanceAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        advanceAction.performed += ctx => ChangeAnimation();

        // Create input action for Escape key (to skip tutorial)
        skipAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        skipAction.performed += ctx => SkipTutorial();  // Skip the tutorial when Escape is pressed
    }

    private void OnStateChange(IExitableState newState)
    {
        if (stateMachine.IsTests) return;
        
        if (newState is GameLoopState && !tutorialStarted)
        {
            StartCoroutine(PlayTutorialAnimationWithDelay(0.6f));
            tutorialStarted = true;
            advanceAction.Enable();
            skipAction.Enable();

            // Disable all action maps
            DisableAllInputMaps();

            stateMachine.ChangeStateAction -= OnStateChange;
        }
    }

    private void ChangeAnimation()
    {
        if (tutorialBox.activeSelf)
        {
            AudioManager.Instance.Play(SoundEnum.Tutorial_paper);
            int currentStep = animtut.GetInteger("Change");
            animtut.SetInteger("Change", currentStep + 1);
        }
    }

    private void SkipTutorial()
    {
        Endtut();  // Skip the tutorial
    }

    private IEnumerator PlayTutorialAnimationWithDelay(float delay)
    {
        tutorialBox.SetActive(false);
        yield return new WaitForSecondsRealtime(delay); // Unscaled time, so it works even if game is paused
        tutorialBox.SetActive(true);
        animtut.enabled = true;
        animtut.Play("Default", 0, 0f);
        AudioManager.Instance.Play(SoundEnum.Tutorial_paper);
    }

    public void Endtut()
    {
        AudioManager.Instance.Play(SoundEnum.General_Click);

        animtut.enabled = false;
        //tutorialBox.SetActive(false);
        advanceAction.Disable();
        skipAction.Disable();

        // Re-enable main game input maps
        EnableGameplayInputMaps();

        gameObject.SetActive(false);
    }

    private void DisableAllInputMaps()
    {
        foreach (var map in inputActions.actionMaps)
        {
            map.Disable();
        }
    }

    private void EnableGameplayInputMaps()
    {
        EnableMapIfExists("Player");
        EnableMapIfExists("UI");
        EnableMapIfExists("Map");
        EnableMapIfExists("Help");
    }

    private void EnableMapIfExists(string mapName)
    {
        var map = inputActions.FindActionMap(mapName, true);
        if (map != null)
        {
            map.Enable();
        }
    }

    private void OnDestroy()
    {
        advanceAction?.Dispose();
        skipAction?.Dispose();
        stateMachine.ChangeStateAction -= OnStateChange;
    }
}
