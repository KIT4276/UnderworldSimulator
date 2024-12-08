using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private const string InitialPointTag = "InitialPoint";

    private readonly StateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly GameFactory _gameFactory;
    private readonly IInputService _input;
    private readonly IPersistantProgressService _progressService;

    private GameObject _playerObj;

    public LoadLevelState(StateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
        GameFactory gameFactory, /*IInputService input,*/ IPersistantProgressService progressService)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        //_input = input;
        _progressService = progressService;
    }

    public void Enter(string sceneName)
    {
        _curtain.Show();
        _gameFactory.CleanUp();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
        _curtain.Hide();

    private void OnLoaded()
    {
        InitGameWorld();
        InformProgressReaders();

        _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            progressReader.LoadProgress(_progressService.Progress);
    }

    private void InitGameWorld()
    {
        //_playerObj = InitPlayer();
        InitHud(_playerObj);//??

        InitSpawners();

        CameraFollow(_playerObj);//??
    }

    private void InitSpawners()
    {
      //todo
    }

    private void Init(string tag)
    {
        foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(tag))
        {
            var spawner = spawnerObject.GetComponent<Spawner>();
            _gameFactory.Register(spawner);
        }
    }

    //private GameObject InitPlayer() =>
    //    _gameFactory.CreatePlayerAt(GameObject.FindWithTag(InitialPointTag), _input);

    private void InitHud(GameObject player) =>
        _gameFactory.CreateHud().GetComponentInChildren<ActorUI>();//.Construct(player.GetComponent<PlayerHealth>());

    private void CameraFollow(GameObject player) =>
        Camera.main.GetComponent<Camera>();//.Follow(player.transform);
}