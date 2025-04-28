using UnityEngine.InputSystem;

public class CheatCodes
{
    private readonly InputActionReference[] _tests;
    private readonly InputActionReference[] _mat_tests;
    private readonly Decor[] _decors;
    private readonly CraftLootSettings[] _craftLootSettings;
    private readonly MaterialsData _materialsData;
    private readonly InventorySystem _inventory;
    private readonly DrawingData _drawingData;
    private readonly GuestsStaticData _guestsData;

    private  bool _drawingsInited;

    public CheatCodes(InventorySystem inventory, DrawingData drawingData, GuestsStaticData guestsData, MaterialsData materialsData,

        InputActionReference[] tests,
        InputActionReference[] mat_tests,
        CraftLootSettings[] craftLootSettings, Decor[] decors)
    {

        _craftLootSettings = craftLootSettings;
        _inventory = inventory;
        _drawingData = drawingData;
        _guestsData = guestsData;
        _decors = decors;
        _materialsData = materialsData;

        _tests = tests;
        _mat_tests = mat_tests;

        Subscribe();
    }

    private void Subscribe()
    {
        _tests[0].action.started += AddAllDecor;
        _tests[1].action.started += AddAllDrawings;
        _tests[2].action.started += AddAllGuests;



        for(int i = 0;  i < _mat_tests.Length; i++)
        {
            _mat_tests[i].action.started += AddMaterial;
        }
    }

    private void AddMaterial(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Craft_Test_1":
               // Debug.Log(_craftLootSettings[0].Loot.LootType);
                AddMaterials(_craftLootSettings[0].Loot);
                break;
            case "Craft_Test_2":
               // Debug.Log(_craftLootSettings[1].Loot.LootType);
                AddMaterials(_craftLootSettings[1].Loot);
                break;
            case "Craft_Test_3":
               // Debug.Log(_craftLootSettings[2].Loot.LootType);
                AddMaterials(_craftLootSettings[2].Loot);
                break;
            case "Craft_Test_4":
               // Debug.Log(_craftLootSettings[3].Loot.LootType);
                AddMaterials(_craftLootSettings[3].Loot);
                break;
            case "Craft_Test_5":
                //Debug.Log(_craftLootSettings[4].Loot.LootType);
                AddMaterials(_craftLootSettings[4].Loot);
                break;
            case "Craft_Test_6":
               // Debug.Log(_craftLootSettings[5].Loot.LootType);
                AddMaterials(_craftLootSettings[5].Loot);
                break;
            case "Craft_Test_7":
               // Debug.Log(_craftLootSettings[6].Loot.LootType);
                AddMaterials(_craftLootSettings[6].Loot);
                break;
            case "Craft_Test_8":
               // Debug.Log(_craftLootSettings[7].Loot.LootType);
                AddMaterials(_craftLootSettings[7].Loot);
                break;
            case "Craft_Test_9":
               // Debug.Log(_craftLootSettings[8].Loot.LootType);
                AddMaterials(_craftLootSettings[8].Loot);
                break;
            case "Craft_Test_10":
               // Debug.Log(_craftLootSettings[9].Loot.LootType);
                AddMaterials(_craftLootSettings[9].Loot);
                break;
            case "Craft_Test_11":
               // Debug.Log(_craftLootSettings[10].Loot.LootType);
                AddMaterials(_craftLootSettings[10].Loot);
                break;
        }
    }

    private void AddMaterials(Item item)
    {
        item.Init(_materialsData);
        _inventory.TryReturnLootToInventory(item);
    }

    private void AddAllGuests(InputAction.CallbackContext context)
    {
        foreach (var guest in _guestsData.Guests)
        {
            if (!guest.IsAvailable)
                guest.MakeAvailable();
        }
    }

    private void AddAllDrawings(InputAction.CallbackContext context)
    {
        if (!_drawingsInited)
        {
            foreach (var drawing in _drawingData.Drawings)
            {
                if (!drawing.IsAvailable)
                {
                    drawing.MakeAvailable();
                }
            }
            _drawingsInited = true;
        }
    }

    private void AddAllDecor(InputAction.CallbackContext context)
    {
        foreach (var decor in _decors)
        {
            _inventory.TryReturnDecorToInventory(decor);
        }

        //for (int i = 0; i < _inventory.InventorySlots.Length; i++)
        //{
        //    if (i < _decors.Length)
        //        _inventory.InventorySlots[i].SetItem(_decors[i]);
        //}
    }
}
