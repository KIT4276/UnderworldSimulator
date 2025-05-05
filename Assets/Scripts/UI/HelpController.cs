using UnityEngine;
using UnityEngine.InputSystem;

public class HelpController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset; // Reference to the InputActionAsset
    [SerializeField] private GameObject helpPanel; // Reference to the map UI GameObject

    private bool isHelpVisible = false; // Track visibility state of the map

    private InputAction toggleHelpAction; // Action to toggle the map
    private InputActionMap playerActionMap; // Reference to the Player action map
    private InputActionMap uiActionMap; // Reference to the UI action map
    private InputActionMap gamemapActionMap;

    private InputAction closeHelpAction;

    private void Awake()
    {
        if (helpPanel != null)
        {
            helpPanel.SetActive(false); // Start with the map hidden
        }

        // Find the Player action map
        playerActionMap = inputActionAsset.FindActionMap("Player"); // Use the correct action map name

        // Find the UI action map
        uiActionMap = inputActionAsset.FindActionMap("UI"); // Use the correct action map name

        gamemapActionMap = inputActionAsset.FindActionMap("Map"); 

        // Find the ToggleHelp action in the separate Map Controls action map
        var helpActionMap = inputActionAsset.FindActionMap("Help");
        toggleHelpAction = helpActionMap.FindAction("ToggleHelp"); // Use the correct action name

        closeHelpAction = helpActionMap.FindAction("CloseHelp");
    }

    private void OnEnable()
    {
        toggleHelpAction.performed += OnToggleHelp; // Subscribe to the toggle map action
        toggleHelpAction.Enable(); // Enable the toggle map action
    }

    private void OnDisable()
    {
        toggleHelpAction.performed -= OnToggleHelp; // Unsubscribe from the toggle map action
        toggleHelpAction.Disable(); // Disable the toggle map action

        closeHelpAction.performed -= OnCloseHelp; 
        closeHelpAction.Disable();
    }

    // This method handles toggling the map
    public void OnToggleHelp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleHelp(); // Call the method to toggle the map
        }
    }

    private void OnCloseHelp(InputAction.CallbackContext context)
    {
        if (context.performed && isHelpVisible)
        {
            ToggleHelp();
        }
    }

    // Method to toggle the map's visibility
    public void ToggleHelp()
    {
        isHelpVisible = !isHelpVisible; // Toggle visibility
        if (helpPanel != null)
        {
            helpPanel.SetActive(isHelpVisible); // Show/hide map
        }

        if (isHelpVisible)
        {
            playerActionMap.Disable(); // Disable the Player action map when the map is visible
            uiActionMap.Disable(); // Disable the UI action map when the map is visible
            gamemapActionMap.Disable();

            closeHelpAction.performed += OnCloseHelp;
            closeHelpAction.Enable();
        }
        else
        {
            playerActionMap.Enable(); // Enable the Player action map when the map is hidden
            uiActionMap.Enable(); // Enable the UI action map when the map is hidden
            gamemapActionMap.Enable();

            closeHelpAction.performed -= OnCloseHelp;
            closeHelpAction.Disable();
        }
    }
}
