using UnityEngine;
using UnityEngine.Tilemaps;

public class AudioReciever : MonoBehaviour
{
    public static AudioReciever Instance;
    public FloorMaterial FloorMaterial;
    [SerializeField] private float _timeToBirdSing;
    private float _timeToBirdSingBase;

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
            if (randomNumber >= 0 && randomNumber < 3) AudioManager.Instance.Play(SoundEnum.Crow);
            if (randomNumber == 3) AudioManager.Instance.Play(SoundEnum.Woodpecker);
            if (randomNumber >= 4) AudioManager.Instance.Play(SoundEnum.Owl);

            _timeToBirdSing = _timeToBirdSingBase + Random.Range(_timeToBirdSingBase * 0.8f, _timeToBirdSingBase * 1.2f);
        }
    }

    public void StartGameplay()
    {
        if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == true) AudioManager.Instance.Stop(SoundEnum.Menu, 2);
        if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == false) AudioManager.Instance.Play(SoundEnum.Gameplay, 2, true);
    }

    public void StartMenu()
    {
        if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == true) AudioManager.Instance.Stop(SoundEnum.Gameplay, 2);
        if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == false) AudioManager.Instance.Play(SoundEnum.Menu, 2, true);
        AudioManager.Instance.Play(SoundEnum.Wind, 2, true);
    }

    public void PlayFootstep()
    {
        if (FloorMaterial == FloorMaterial.Leaves) AudioManager.Instance.Play(SoundEnum.Footstep_Leaves);
        if (FloorMaterial == FloorMaterial.Rocks) AudioManager.Instance.Play(SoundEnum.Footstep_Rocks);
        if (FloorMaterial == FloorMaterial.Dirt) AudioManager.Instance.Play(SoundEnum.Footstep_Dirt);
        if (FloorMaterial == FloorMaterial.Wood) AudioManager.Instance.Play(SoundEnum.Footstep_Wood);
    }

}
