using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] SoundEnum _sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hero") AudioManager.Instance.Play(_sound);
    }
}
