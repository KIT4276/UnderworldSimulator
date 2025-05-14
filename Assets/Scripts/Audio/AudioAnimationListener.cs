using UnityEngine;
using DragonBones;

public class AudioAnimationListener : MonoBehaviour
{
    public UnityArmatureComponent armatureComponent;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        armatureComponent.AddDBEventListener(EventObject.FRAME_EVENT, OnFrameEvent);
        armatureComponent.AddDBEventListener(EventObject.COMPLETE, OnMoveAnimationComplete);
    }

    void OnFrameEvent(string type, EventObject eventObject)
    {
        // Debug.Log("Frame event triggered: " + eventObject.name);

        // if (eventObject.name == "footstep_right" || eventObject.name == "footstep_left" || eventObject.name == "footstep_side_front"
        // || eventObject.name == "footstep_side_back" || eventObject.name == "footstep_back_left" || eventObject.name == "footstep_back_right")
        // {
        //     AudioReciever.Instance.PlayFootstep();
        // }

        if (eventObject.name == "start_id_wolf")
        {
            AudioManager.Instance.PlayOneShot(SoundEnum.NPC_Wolf_Action, _audioSource);
        }
        if (eventObject.name == "start_id_bull" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.NPC_Bull_Action, _audioSource);
        if (eventObject.name == "start_id_bear_01" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.Bear_Start, _audioSource);
        if (eventObject.name == "start_id_bear_02" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.Bear_Finish, _audioSource);
        if (eventObject.name == "start_id_rabbit" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.NPC_Bunny_Action, _audioSource);
        if (eventObject.name == "01_start_id_monkey" && _audioSource != null) if (!AudioManager.Instance.IsPlaying(SoundEnum.hotel_npc_monkey_scream)) AudioManager.Instance.Play(SoundEnum.hotel_npc_monkey_scream, _audioSource, 0, true);
        if (eventObject.name == "02_start_id_monkey" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.Monkey_Step, _audioSource);
    }

    protected void OnMoveAnimationComplete(string type, EventObject eventObject)
    {
        // Debug.Log(eventObject.animationState.name);

        if (eventObject.animationState.name == "idle_animation_01")
        {
            AudioManager.Instance.Stop(SoundEnum.hotel_npc_monkey_scream, 1.5f);
        }
    }
}
