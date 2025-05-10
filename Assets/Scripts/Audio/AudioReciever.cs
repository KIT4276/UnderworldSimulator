using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class AudioReciever : MonoBehaviour
{
    public static AudioReciever Instance;
    public FloorMaterial FloorMaterial;

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

    public void StartGameplay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == true) AudioManager.Instance.Stop(SoundEnum.Menu, 2);
            if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == false) AudioManager.Instance.Play(SoundEnum.Gameplay, 2, true);
        }
    }

    public void StartMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (AudioManager.Instance.IsPlaying(SoundEnum.Gameplay) == true) AudioManager.Instance.Stop(SoundEnum.Gameplay, 2);
            if (AudioManager.Instance.IsPlaying(SoundEnum.Menu) == false) AudioManager.Instance.Play(SoundEnum.Menu, 2, true);
            AudioManager.Instance.Play(SoundEnum.Wind, 2, true);
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
        // AudioManager.Instance.Play(SoundEnum.Search_Object);
    }
}
