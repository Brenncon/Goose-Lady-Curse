using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]

public class InputReader : SerializableScriptableObject, InputMap.IGameplayActions
{
    public event UnityAction<Vector3> MovementEvent = delegate { };
    public event UnityAction<GameObject> AttackEvent = delegate { };
    public event UnityAction<GameObject> PickupEvent = delegate { };
    public event UnityAction InteractEvent = delegate { };
    public event UnityAction<float> ZoomEvent = delegate { };
    public event UnityAction OpenInventoryEvent = delegate { };
    public event UnityAction OpenCharacterPanelEvent = delegate { };
    public event UnityAction OpenCraftingMenuEvent = delegate { };
    public event UnityAction OpenHelpMenuEvent = delegate { };
    public event UnityAction OpenPauseMenuEvent = delegate { };
    public event UnityAction OpenBuildMenuEvent = delegate { };

    private InputMap _gameInput;
    private Ray ray;
    private RaycastHit hit;
    private LayerMask TerrainLayerMask;
    private LayerMask AttackableLayerMask;
    private LayerMask ItemLayerMask;
    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new InputMap();
            _gameInput.Gameplay.SetCallbacks(this);
        }
        TerrainLayerMask = LayerMask.GetMask("Terrain");
        AttackableLayerMask = LayerMask.GetMask("Attackable");
        ItemLayerMask = LayerMask.GetMask("Item");
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnMovements(InputAction.CallbackContext context)
    {
        Debug.Log("click");
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (context.phase == InputActionPhase.Performed && Physics.Raycast(ray, out hit, 500f, TerrainLayerMask))
        {
            MovementEvent.Invoke(hit.point);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (context.phase == InputActionPhase.Performed && Physics.Raycast(ray, out hit, 500f, AttackableLayerMask))
        {
            AttackEvent.Invoke(hit.collider.gameObject);
        }
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (context.phase == InputActionPhase.Performed && Physics.Raycast(ray, out hit, 500f, ItemLayerMask))
        {
            PickupEvent.Invoke(hit.collider.gameObject);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent.Invoke();
        }
    }

    public void OnZoomCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ZoomEvent.Invoke(context.ReadValue<float>());
        }
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenInventoryEvent.Invoke();
        }
    }

    public void OnOpenCharacterPanel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenCharacterPanelEvent.Invoke();
        }
    }

    public void OnOpenCraftingMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenCraftingMenuEvent.Invoke();
        }
    }

    public void OnOpenHelpMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenHelpMenuEvent.Invoke();
        }
    }

    public void OnOpenPauseMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenPauseMenuEvent.Invoke();
        }
    }

    public void OnOpenBuildMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OpenBuildMenuEvent.Invoke();
        }
    }

    public void DisableAllInput()
    {
        _gameInput.Gameplay.Disable();
    }
}