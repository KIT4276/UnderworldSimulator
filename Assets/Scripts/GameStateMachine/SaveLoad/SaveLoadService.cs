using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";

    private readonly IPersistantProgressService _progressService;
    private readonly GameFactory _gameFactory;

    public SaveLoadService(IPersistantProgressService progressService, GameFactory gameFactory)
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        {
            progressWriter.SaveProgress(_progressService.Progress);
          // Debug.Log(progressWriter);
        }

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
        return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}