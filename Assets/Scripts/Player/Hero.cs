using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Hero : MonoBehaviour
    {

    [SerializeField] private HeroMove _heroMove;
    [SerializeField] private HeroAnimator _animator;
   // [SerializeField] private PlayerInput _playerInput;

    public void Initialize(StateMachine stateMachine, DiContainer container, PersistantStaticData staticData)
    {
        _heroMove.Initialize(stateMachine);
        _animator.Initialize(stateMachine, staticData);
        container.Bind<HeroMove>().AsSingle();

       // container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle().NonLazy();
    }

    public void OnLoot()
    {
        _animator.PlayLoot();
    }
}