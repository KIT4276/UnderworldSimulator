using UnityEngine;
using UnityEngine.InputSystem;

public class MapController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset; // Reference to the InputActionAsset
    [SerializeField] private GameObject mapPanel; // Reference to the map UI GameObject

    private bool isMapVisible = false; // Track visibility state of the map

    private InputAction toggleMapAction; // Action to toggle the map
    private InputActionMap playerActionMap; // Reference to the Player action map
    private InputActionMap uiActionMap; // Reference to the UI action map
    private InputActionMap helpActionMap; // Reference to the Help action map


    private void Awake()
    {
        if (mapPanel != null)
        {
            mapPanel.SetActive(false); // Start with the map hidden
        }

        // Find the Player action map
        playerActionMap = inputActionAsset.FindActionMap("Player");

        // Find the UI action map
        uiActionMap = inputActionAsset.FindActionMap("UI"); 

        // Find the Help action map
        helpActionMap = inputActionAsset.FindActionMap("Help"); 

        // Find the ToggleMap action in the separate Map Controls action map
        var mapControlsActionMap = inputActionAsset.FindActionMap("Map");
        toggleMapAction = mapControlsActionMap.FindAction("ToggleMap"); // Use the correct action name
    }

    private void OnEnable()
    {
        toggleMapAction.performed += OnToggleMap; // Subscribe to the toggle map action
        toggleMapAction.Enable(); // Enable the toggle map action
    }

    private void OnDisable()
    {
        toggleMapAction.performed -= OnToggleMap; // Unsubscribe from the toggle map action
        toggleMapAction.Disable(); // Disable the toggle map action
    }

    // This method handles toggling the map
    public void OnToggleMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleMap(); // Call the method to toggle the map
        }
    }

    // Method to toggle the map's visibility
    private void ToggleMap()
    {
        isMapVisible = !isMapVisible; // Toggle visibility
        if (mapPanel != null)
        {
            mapPanel.SetActive(isMapVisible); // Show/hide map
        }

        if (isMapVisible)
        {
            playerActionMap.Disable(); // Disable the Player action map when the map is visible
            uiActionMap.Disable(); // Disable the UI action map when the map is visible
            helpActionMap.Disable(); // Disable the Help action map when the map is visible
        }
        else
        {
            playerActionMap.Enable(); // Enable the Player action map when the map is hidden
            uiActionMap.Enable(); // Enable the UI action map when the map is hidden
            helpActionMap.Enable(); // Enable the UI action map when the map is visible
        }
    }
}
