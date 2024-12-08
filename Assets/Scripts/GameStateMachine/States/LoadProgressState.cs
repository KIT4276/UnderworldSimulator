public class LoadProgressState : IState
{
    private const string Main = "Main";

    private readonly StateMachine _gameStateMachine;
    private readonly IPersistantProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly PersistantPlayerStaticData _persistantPlayerStaticData;

    public LoadProgressState(StateMachine gameStateMachine, IPersistantProgressService progressService,
        ISaveLoadService saveLoadService, PersistantPlayerStaticData persistantPlayerStaticData)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _persistantPlayerStaticData = persistantPlayerStaticData;
    }

    public void Enter()
    {
        LoadProgressOrInitNew();
        _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
    }

    public void Exit() { }

    private void LoadProgressOrInitNew() =>
        _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

    private PlayerProgress NewProgress()
    {
        var progress = new PlayerProgress(initialLevel: Main);

        return progress;
    }
}