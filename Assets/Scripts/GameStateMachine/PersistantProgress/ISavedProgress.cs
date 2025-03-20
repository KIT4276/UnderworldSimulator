public interface ISavedProgressReader
{
    void LoadProgress(PlayerProgress progress);
}


public interface ISavedProgress : ISavedProgressReader
{
    void SaveProgress(PlayerProgress progress);
}
