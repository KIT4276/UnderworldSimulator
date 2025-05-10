using UnityEngine;

public class AudioBush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero") if (!AudioManager.Instance.IsPlaying(SoundEnum.Bush)) AudioManager.Instance.Play(SoundEnum.Bush);
    }
}
