using UnityEngine;
using Zenject;

public class Hero : MonoBehaviour, ISavedProgress
{
    private const string Main = "Main";
    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private HeroAnimator _animator;

    public void Immobilize()
    {
        _heroMove.Immobilize();
    }

    public void Initialize(StateMachine stateMachine, DiContainer container, PersistantStaticData staticData)
    {
        _heroMove.Initialize(stateMachine);
        _animator.Initialize(stateMachine, staticData);
        container.Bind<HeroMove>().AsSingle();
    }

    public void LoadProgress(PlayerProgress progress)
    {

        if (progress.WorldData != null)
        {
            PositionOnLevel positionOnLevel = progress.WorldData.PositionOnLevel;
            if (positionOnLevel != null)
            {
                string level = positionOnLevel.Level;

                Vector3Data positionData = positionOnLevel.Position;
                if (positionData != null)
                {
                    transform.position = new Vector3(positionData.X, positionData.Y, positionData.Z);
                }
            }
        }
    }

    public void OnLoot()
    {
        _animator.PlayLoot();
    }

    public void SaveProgress(PlayerProgress progress)
    {
        if (progress.WorldData != null)
        {
            PositionOnLevel positionOnLevel = progress.WorldData.PositionOnLevel;

            positionOnLevel.Level = Main;
            positionOnLevel.Position = new Vector3Data(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}