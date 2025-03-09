using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LootInteract : InteractableObstacle
{
    // [SerializeField] private float _interactionTime = 1;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _bar;
    [SerializeField] private CraftLoot _craftLoot;
    [SerializeField] private QuestsLoot _questsLoot;

    [Inject] private LootSystem _lootSystem;
    [Inject] private PersistantStaticData _staticData;
    [Inject] private StateMachine _machine;

    private Coroutine _interactionCoroutine;
    private bool _IsFilled;

    private void Start()
    {
        _bar.fillAmount = 0;
        _progressBar.SetActive(false);
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

    private void FillLoot(LootSettings lootSetting)
    {
        _lootSystem.FillSlot(lootSetting.Loot, lootSetting.Count, this.gameObject);

    }

    private IEnumerator InteractionProgress()
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

    private void OpenMenu()
    {
        _bar.fillAmount = 1f;
        _lootSystem.OpenMenu();
        _progressBar.SetActive(false);
    }
}
