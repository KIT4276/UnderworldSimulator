using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LootInteract : InteractableObstacle
{
    [SerializeField] protected float _respawnTime = 3;
   // [SerializeField] protected GameObject _progressBar;
    //[SerializeField] protected Image _bar;
    [SerializeField] protected CraftLoot _craftLoot;
    // [SerializeField] protected QuestsLoot _questsLoot;
    [SerializeField] protected GameObject _sprite;
    [SerializeField] private Collider2D _interactableCollider;
    [SerializeField] private GoParticleSystem _goParticleSystem;
    [SerializeField] private bool _isOrganic;

    private bool _isComposite = false;

    public Action Interacted;

    [Inject] protected LootSystem _lootSystem;
    [Inject] protected PersistantStaticData _staticData;
    [Inject] protected StateMachine _machine;

    //protected Coroutine _interactionCoroutine;

    protected void Start()
    {
        Restart();
    }

    public void SetComposite() =>
        _isComposite = true;

    public virtual void Restart()
    {
        //_bar.fillAmount = 0;
       // _progressBar.SetActive(false);
    }

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
        Restart();
        _craftLoot.Restart();
    }

    protected override void Interac()
    {
        if (_machine.ActiveState is LootState) return;

        //_progressBar.SetActive(true);


        //if (_interactionCoroutine == null)
        //{
        _hero.GetHero.OnLoot();
        //_interactionCoroutine = StartCoroutine(InteractionProgress());
        // }
        if (!_isComposite)
        {
            DoLoot();

        }
        else
        {
            Interacted?.Invoke();
        }

    }

    public void DoLoot()
    {

        OpenMenu();

        if (_isOrganic) AudioReciever.Instance.PlaySearchOrganic();
        else AudioReciever.Instance.PlaySearchObject();
        _lootSystem.CleanAllSlots();

        _lootSystem.FillName(_craftLoot.Nane);
        foreach (var lootSetting in _craftLoot.LootSettings)
        {
            FillLoot(lootSetting);
        }
    }

    protected virtual void FillLoot(LootSettings lootSetting)
    {

        _lootSystem.FillSlot(lootSetting.Loot, ((CraftLootSettings)lootSetting).CurrentCount, this, _craftLoot);
    }

    //protected IEnumerator InteractionProgress()
    //{
    //    _hero.GetHero.Immobilize();

    //    float elapsedTime = 0f;
    //   // _bar.fillAmount = 0f;

    //    while (elapsedTime < _staticData.LootInteractTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //      //  _bar.fillAmount = Mathf.Clamp01(elapsedTime / _staticData.LootInteractTime);
    //        yield return null;
    //    }

    //    OpenMenu();
    //    _interactionCoroutine = null;
    //}

    protected void OpenMenu()
    {
        // _bar.fillAmount = 1f;
        _lootSystem.OpenMenu();
       // _progressBar.SetActive(false);
    }
}
