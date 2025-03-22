using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LootInteract : InteractableObstacle
{
    [SerializeField] protected float _respawnTime = 3;
    [SerializeField] protected GameObject _progressBar;
    [SerializeField] protected Image _bar;
    [SerializeField] protected CraftLoot _craftLoot;
    [SerializeField] protected QuestsLoot _questsLoot;
    [SerializeField] protected GameObject _sprite;
    [SerializeField] private Collider2D _interactableCollider;

    [Inject] protected LootSystem _lootSystem;
    [Inject] protected PersistantStaticData _staticData;
    [Inject] protected StateMachine _machine;

    protected Coroutine _interactionCoroutine;
    protected bool _IsFilled;

    protected void Start()
    {
        Restart();
    }

    public virtual void Restart()
    {
        _bar.fillAmount = 0;
        _IsFilled = false;
        _progressBar.SetActive(false);
    }

    public virtual void Despawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    protected virtual IEnumerator RespawnRoutine()
    {
        _sprite.SetActive(false);
        _interactableCollider.enabled = false;
        yield return new WaitForSeconds(_respawnTime);
        _sprite.SetActive(true);
        _interactableCollider.enabled = true;
        Restart();
    }

    protected override void Interac()
    {
        if (_machine.ActiveState is LootState) return;

        _progressBar.SetActive(true);


        if (_interactionCoroutine == null)
        {
            _hero.GetHero.OnLoot();
            _interactionCoroutine = StartCoroutine(InteractionProgress());
        }
        if (!_IsFilled)
        {
            foreach (var lootSetting in _craftLoot.LootSettings)
            {
                FillLoot(lootSetting);
            }
            foreach (var lootSetting in _questsLoot.LootSettings)
            {
                FillLoot(lootSetting);
            }
            _IsFilled = true;
        }
    }

    protected virtual void FillLoot(LootSettings lootSetting)
    {
        _lootSystem.FillSlot(lootSetting.Loot, lootSetting.Count, this, _respawnTime);
    }

    protected IEnumerator InteractionProgress()
    {
        _hero.GetHero.Immobilize();

        float elapsedTime = 0f;
        _bar.fillAmount = 0f;

        while (elapsedTime < _staticData.LootInteractTime)
        {
            elapsedTime += Time.deltaTime;
            _bar.fillAmount = Mathf.Clamp01(elapsedTime / _staticData.LootInteractTime);
            yield return null;
        }

        OpenMenu();
        _interactionCoroutine = null;
    }

    protected void OpenMenu()
    {
        _bar.fillAmount = 1f;
        _lootSystem.OpenMenu();
        _progressBar.SetActive(false);
    }
}
