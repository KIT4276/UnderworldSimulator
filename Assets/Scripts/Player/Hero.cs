using UnityEngine;
using Zenject;

public class Hero : MonoBehaviour, ISavedProgress
{

    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private HeroAnimator _animator;
   // [SerializeField] private PlayerInput _playerInput;

   // public HeroMove HeroMove { get => _heroMove; }

    public void Immobilize()
    {
        _heroMove.Immobilize();
    }

    public void Initialize(StateMachine stateMachine, DiContainer container, PersistantStaticData staticData)
    {
        _heroMove.Initialize(stateMachine);
        _animator.Initialize(stateMachine, staticData);
        container.Bind<HeroMove>().AsSingle();

       // container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle().NonLazy();
    }

    public void LoadProgress(PlayerProgress progress)
    {
        Debug.Log("Hero LoadProgress");

        // Загружаем данные о положении героя из PlayerProgress
        if (progress.WorldData != null)
        {
            PositionOnLevel positionOnLevel = progress.WorldData.PositionOnLevel;
            if (positionOnLevel != null)
            {
                // Устанавливаем уровень
                string level = positionOnLevel.Level;

                // Устанавливаем позицию героя
                Vector3Data positionData = positionOnLevel.Position;
                if (positionData != null)
                {
                    transform.position = new Vector3(positionData.X, positionData.Y, positionData.Z);
                }

                Debug.Log($"Hero loaded at level: {level} with position: {transform.position}");
            }
        }

    }

    public void OnLoot()
    {
        _animator.PlayLoot();
    }

    public void SaveProgress(PlayerProgress progress)
    {
        Debug.Log("Hero SaveProgress");

        // Сохраняем данные о положении героя в PlayerProgress
        if (progress.WorldData != null)
        {
            PositionOnLevel positionOnLevel = progress.WorldData.PositionOnLevel;

            // Обновляем уровень и позицию
            positionOnLevel.Level = "Main";
            positionOnLevel.Position = new Vector3Data(transform.position.x, transform.position.y, transform.position.z);

            Debug.Log($"Hero saved at level: {positionOnLevel.Level} with position: {transform.position}");
        }
    }
}