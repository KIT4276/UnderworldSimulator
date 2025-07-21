using System;
using UnityEngine;

public class CompositeLoot : MonoBehaviour 
{
    [SerializeField] private LootInteract _lootInteract;

    public Action Interacted;

    private void Awake()
    {
        _lootInteract.SetComposite();

        _lootInteract.InteractedCompositeLoot += OpenMiniGame;
    }


    private void OpenMiniGame()
    {
        Interacted?.Invoke();
    }
}
