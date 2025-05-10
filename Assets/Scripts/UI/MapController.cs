using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class MapController : MonoBehaviour
{

    [Header("Input System References")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private GameObject mapPanel;

    [Header("Map Components")]
    [SerializeField] private RectTransform mapImage;
    [SerializeField] private RectTransform heroIcon;
    private Transform playerTransform;

    [Header("World Bounds")]
    [SerializeField] private Vector2 worldMin = new Vector2(-50, -50);
    [SerializeField] private Vector2 worldMax = new Vector2(50, 50);

    [Header("MiniMap Components")]
    [SerializeField] private RectTransform mapMiniImage;
    [SerializeField] private RectTransform heroMiniIcon;
    private Transform playerMiniTransform;
    [SerializeField] private GameObject _miniMap;

    [Header("Minimap World Bounds")]
    [SerializeField] private Vector2 worldMiniMin = new Vector2();
    [SerializeField] private Vector2 worldMiniMax = new Vector2();

    private bool isMapVisible = false;
    private StateMachine stateMachine;


    private InputAction toggleMapAction;
    private InputActionMap playerActionMap;
    private InputActionMap uiActionMap;
    private InputActionMap helpActionMap;
    private InputAction closeMapAction;

    [Inject]
    public void Construct(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        stateMachine.ChangeStateAction += OnStateChange;
    }

    private void Awake()
    {
        playerActionMap = inputActionAsset.FindActionMap("Player");
        uiActionMap = inputActionAsset.FindActionMap("UI");
        helpActionMap = inputActionAsset.FindActionMap("Help");

        var mapControlsActionMap = inputActionAsset.FindActionMap("Map");
        toggleMapAction = mapControlsActionMap.FindAction("ToggleMap");
        closeMapAction = mapControlsActionMap.FindAction("CloseMap");
    }

    private void OnEnable()
    {
        toggleMapAction.performed += OnToggleMap;
        toggleMapAction.Enable();
    }

    private void OnDisable()
    {
        toggleMapAction.performed -= OnToggleMap;
        toggleMapAction.Disable();

        closeMapAction.performed -= OnCloseMap;
        closeMapAction.Disable();
    }

    private void OnCloseMap(InputAction.CallbackContext context)
    {
        if (context.performed && isMapVisible)
        {
            ToggleMap(); // ��������� �����
        }
    }

    private void OnStateChange(IExitableState newState)
    {
        // Check if the state is GameLoopState
        if (newState is GameLoopState)
        {
            // GameLoopState has been entered, initialize the player and map
            InitializePlayerAndMap();

            // Unsubscribe from the event as we no longer need to listen for state changes
            stateMachine.ChangeStateAction -= OnStateChange;
        }
    }

    private void InitializePlayerAndMap()
    {
        // Now that we are in the GameLoopState, find the player and initialize the map
        GameObject playerObject = GameObject.FindWithTag("Hero");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            // Initialize map logic or UI setup here if needed
            if (mapPanel != null)
            {
                mapPanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }
    }

    private void OnToggleMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleMap();
        }
    }

    public void ToggleMap()
    {
        isMapVisible = !isMapVisible;
        if (mapPanel != null)
        {
            mapPanel.SetActive(isMapVisible);
        }

        if (isMapVisible)
        {
            UpdateHeroIconPosition();
            playerActionMap.Disable();
            uiActionMap.Disable();
            helpActionMap.Disable();

            closeMapAction.performed += OnCloseMap;
            closeMapAction.Enable();
        }
        else
        {
            playerActionMap.Enable();
            uiActionMap.Enable();
            helpActionMap.Enable();

            closeMapAction.performed -= OnCloseMap;
            closeMapAction.Disable();
        }
    }

    // This method maps the player's world position to the map image and moves the icon
    private void UpdateHeroIconPosition()
    {
        if (playerTransform != null)
        {
            Vector3 playerPos = playerTransform.position;

            // Normalize player position (0 to 1 range)
            float normalizedX = Mathf.InverseLerp(worldMin.x, worldMax.x, playerPos.x);
            float normalizedY = Mathf.InverseLerp(worldMin.y, worldMax.y, playerPos.y); // Use Y for top-down

            // Get the size of the map image
            Vector2 mapSize = mapImage.rect.size;

            // Calculate local position for HeroIcon
            float mapPosX = (normalizedX - 0.5f) * mapSize.x;
            float mapPosY = (normalizedY - 0.5f) * mapSize.y;

            // Apply to HeroIcon
            heroIcon.anchoredPosition = new Vector2(mapPosX, mapPosY);
        }
    }

    private void OnDestroy()
    {
        if (stateMachine != null)
            stateMachine.ChangeStateAction -= OnStateChange;
    }

    private void UpdatetHeroIconMinimapPosition()
    {
        Vector3 playerMiniPos;
        if (playerTransform != null) playerMiniPos = playerTransform.position;
        else playerMiniPos = new Vector3(0, 0, 0);

        // Normalize player position (0 to 1 range)
        float normalizedX = Mathf.InverseLerp(worldMin.x, worldMiniMax.x, playerMiniPos.x);
        float normalizedY = Mathf.InverseLerp(worldMin.y, worldMiniMax.y, playerMiniPos.y); // Use Y for top-down

        // Get the size of the map image
        Vector2 mapMiniSize = mapMiniImage.rect.size;

        // Calculate local position for HeroIcon
        float mapMiniPosX = (normalizedX - 0.5f) * mapMiniSize.x;
        float mapMiniPosY = (normalizedY - 0.5f) * mapMiniSize.y;

        // Apply to HeroIcon
        heroMiniIcon.anchoredPosition = new Vector2(mapMiniPosX, mapMiniPosY);
    }
    private void Update()
    {
        UpdatetHeroIconMinimapPosition();
    }
    // public void ToggleMinimap()
    // {
    //     if (isMapVisible)
    //     {
    //         _miniMap.SetActive(true);
    //     }
    //     else
    //     {
    //         _miniMap.SetActive(false);
    //     }
    // }
}
