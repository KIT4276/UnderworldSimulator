using UnityEngine;
using Zenject;

public class LootInteract : InteractableObstacle
{
    [Inject] private LootSystem _lootSystem;

    protected override void Interac()
    {
        _lootSystem.OpenMenu();
    }
}
