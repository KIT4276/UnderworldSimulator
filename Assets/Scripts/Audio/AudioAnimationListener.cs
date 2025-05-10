using UnityEngine;
using DragonBones;

public class AudioAnimationListener : MonoBehaviour
{
    public UnityArmatureComponent armatureComponent;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        armatureComponent.AddDBEventListener(EventObject.FRAME_EVENT, OnFrameEvent);
    }

    void OnFrameEvent(string type, EventObject eventObject)
    {
        Debug.Log("Frame event triggered: " + eventObject.name);

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
        // if (eventObject.name == "start_id_monkey_01" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.Monkey_Step, _audioSource);
        if (eventObject.name == "02_start_id_monkey" && _audioSource != null) AudioManager.Instance.Play(SoundEnum.Monkey_Step, _audioSource);
    }
}
