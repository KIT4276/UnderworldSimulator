using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class LootInteract : InteractableObstacle
{
    [SerializeField] protected float _respawnTime = 3;
    [SerializeField] protected CraftLoot _craftLoot;
    [SerializeField] protected GameObject _sprite;
    [SerializeField] private Collider2D _interactableCollider;
    [SerializeField] private GoParticleSystem _goParticleSystem;
    [SerializeField] private LootMiniGameUI _miniGameUI;
    [SerializeField] private bool _isOrganic;

    private bool _isComposite = false;

    public Action Interacted;

    [Inject] protected LootSystem _lootSystem;
    [Inject] protected PersistantStaticData _staticData;
    [Inject] protected StateMachine _machine;

    public void SetComposite() =>
        _isComposite = true;

    public virtual void Despawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    protected virtual IEnumerator RespawnRoutine()
    {
        _goParticleSystem.GoAnimate();
        _sprite.SetActive(false);
        _interactableCollider.enabled = false;
        yield return new WaitForSeconds(_respawnTime);
        _sprite.SetActive(true);
        _interactableCollider.enabled = true;
        _craftLoot.Restart();
    }

    protected override void Interac()
    {
        if (_machine.ActiveState is LootState) return;

        _hero.GetHero.OnLoot();

        if (!_isComposite)
        {
            DoLoot();
        }
        else
        {
            Interacted?.Invoke();
        }
    }

    private void DoLoot()
    {
        StartFillLoot();
        foreach (var lootSetting in _craftLoot.LootSettings)
        {
            FillLoot(lootSetting);
        }
    }

    public void DoLoot(int count)
    {
        if (count == 0)
        {
            _lootSystem.OffInteractiveObject();
            return;
        }
        StartFillLoot();

        double result = (double)count * _craftLoot.LootSettings.Length / _miniGameUI.MiniGameStages.Length;
        int maxI = (int)Math.Round(result);
        if (maxI < 1) maxI = 1;

        for (var i = 0; i< maxI; i++)
        {
            FillLoot(_craftLoot.LootSettings[i]);
        }
    }

    private void StartFillLoot()
    {
        OpenMenu();

        if (_isOrganic) AudioReciever.Instance.PlaySearchOrganic();
        else AudioReciever.Instance.PlaySearchObject();

        _lootSystem.CleanAllSlots();

        _lootSystem.FillName(_craftLoot.Nane);
    }

    protected virtual void FillLoot(LootSettings lootSetting)
    {
        _lootSystem.FillSlot(lootSetting.Loot, ((CraftLootSettings)lootSetting).CurrentCount, this, _craftLoot);
    }

    protected void OpenMenu()
    {
        _lootSystem.OpenMenu();
    }
}
