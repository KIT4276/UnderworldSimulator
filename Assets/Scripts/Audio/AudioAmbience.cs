using UnityEngine;

public class AudioAmbience : MonoBehaviour
{
    public AudioSource _birdAudioSource;
    [SerializeField] private float _timeToBirdSing;
    private float _timeToBirdSingBase;

    private void Start()
    {
        _timeToBirdSingBase = _timeToBirdSing;
    }
    private void Update()
    {
        _timeToBirdSing -= Time.deltaTime;
        if (_timeToBirdSing <= 0)
        {
            int randomNumber = Random.Range(0, 5);

            int flip = Random.Range(0, 2);
            int flipTwo = Random.Range(0, 2);
            Vector3 randomPosition = new Vector3(Random.Range(3, 7), Random.Range(3, 7), Camera.main.transform.position.z);

            if (flip == 1) randomPosition = new Vector3(randomPosition.x * -1, randomPosition.y, randomPosition.z);
            if (flipTwo == 1) randomPosition = new Vector3(randomPosition.x, randomPosition.y * -1, randomPosition.z);
            _birdAudioSource.transform.localPosition = randomPosition;

            if (randomNumber >= 0 && randomNumber < 3) AudioManager.Instance.Play(SoundEnum.Crow, _birdAudioSource);
            if (randomNumber == 3) AudioManager.Instance.Play(SoundEnum.Woodpecker, _birdAudioSource);
            if (randomNumber >= 4) AudioManager.Instance.Play(SoundEnum.Owl, _birdAudioSource);

            _timeToBirdSing = Random.Range(_timeToBirdSingBase * 0.6f, _timeToBirdSingBase * 1.3f);
        }
    }
}
