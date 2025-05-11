using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections;
using Zenject;

public class AudioReciever : MonoBehaviour
{
    public static AudioReciever Instance;
    public FloorMaterial FloorMaterial;
    private StateMachine stateMachine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [Inject]
    public void Construct(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachine.ChangeStateAction += OnStateChange;
    }
    private void OnEnable()
    {

        stateMachine.ChangeStateAction += OnStateChange;
    }
    private void OnDisable()
    {

        stateMachine.ChangeStateAction -= OnStateChange;
    }

    private void OnStateChange(IExitableState newState)
    {
        if (newState is GameLoopState)
        {
            if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == true) AudioManager.Instance.Stop(SoundEnum.Menu, 2);
            if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == false) AudioManager.Instance.Play(SoundEnum.Gameplay, 2, true);
            if (AudioManager.Instance.IsPlaying(SoundEnum.Wind) == false) AudioManager.Instance.Play(SoundEnum.Wind, 2, true);
        }

        //WorkbenchState
        //PseudoCraftState
    }

    // public void StartGameplay()
    // {
    //     if (SceneManager.GetActiveScene().buildIndex == 1)
    //     {
    //         if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == true) AudioManager.Instance.Stop(SoundEnum.Menu, 2);
    //         if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == false) AudioManager.Instance.Play(SoundEnum.Gameplay, 2, true);
    //         AudioManager.Instance.Play(SoundEnum.Wind, 2, true);
    //     }
    // }

    public void StartMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == true) AudioManager.Instance.Stop(SoundEnum.Gameplay, 2);
            if (AudioManager.Instance.IsPlaying(SoundEnum.Wind) == true) AudioManager.Instance.Stop(SoundEnum.Wind, 2);
            if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == false) AudioManager.Instance.Play(SoundEnum.Menu, 2, true);
        }
    }

    public void PlayFootstep()
    {
        if (FloorMaterial == FloorMaterial.Leaves) AudioManager.Instance.Play(SoundEnum.Footstep_Leaves);
        if (FloorMaterial == FloorMaterial.Rocks) AudioManager.Instance.Play(SoundEnum.Footstep_Rocks);
        if (FloorMaterial == FloorMaterial.Dirt) AudioManager.Instance.Play(SoundEnum.Footstep_Dirt);
        if (FloorMaterial == FloorMaterial.Wood) AudioManager.Instance.Play(SoundEnum.Footstep_Wood);
    }
    public void PlayUIGeneralClick(string DebugTest)
    {
        // Debug.Log(DebugTest);
        AudioManager.Instance.Play(SoundEnum.General_Click);
    }
    public void PlayUIGeneralHover()
    {
        AudioManager.Instance.Play(SoundEnum.General_Hover);
    }
    public void PlayUIRoomClick()
    {
        AudioManager.Instance.Play(SoundEnum.Room_Choose);
    }
    public void PlayUIBackClick()
    {
        AudioManager.Instance.Play(SoundEnum.Room_Choose);
    }
    public void PlayUIFurnitureClick()
    {
        AudioManager.Instance.Play(SoundEnum.Furniture_Click);
    }
    public void PlayUIFurnitureRotate()
    {
        AudioManager.Instance.Play(SoundEnum.Furniture_Rotate);
    }
    public void PlayUIFurniturePlace()
    {
        AudioManager.Instance.Play(SoundEnum.Furniture_Place);
    }
    public void PlaySearchOrganic()
    {
        AudioManager.Instance.Play(SoundEnum.Search_Organic);
    }
    public void PlaySearchObject()
    {
        AudioManager.Instance.Play(SoundEnum.Search_Object);
    }
    public void PlayUISettingsClick()
    {
        AudioManager.Instance.Play(SoundEnum.General_Click);
    }
    public void PlayMilestoneReached()
    {
        StartCoroutine(MilestoneReached());
    }
    private IEnumerator MilestoneReached()
    {
        AudioManager.Instance.Play(SoundEnum.Milestone_Reached);

        float duration = 2f;
        float elapsed = 0f;
        float value = AudioManager.Instance.GetChannelVolume(ChannelEnum.Music);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            value = Mathf.Lerp(1f, 0f, elapsed / duration);
            AudioManager.Instance.SetChannelVolume(ChannelEnum.Music, value);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        duration = 2f;
        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            value = Mathf.Lerp(0f, 1f, elapsed / duration);
            AudioManager.Instance.SetChannelVolume(ChannelEnum.Music, value);
            yield return null;
        }
    }
}
