using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerControls playerControls;

    public Vector2 MoveInput { get; private set; }
    public bool IsInteracting { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        playerControls = new PlayerControls();

        playerControls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        playerControls.Player.Interact.performed += ctx => IsInteracting = true;
        playerControls.Player.Interact.canceled += ctx => IsInteracting = false;
    }

    private void OnEnable() => playerControls.Enable();
    private void OnDisable()
    {
        if (playerControls != null) {
            playerControls.Disable();
        }
    }

    public void ToggleInput(bool enable)
    {
        if (enable) playerControls.Enable();
        else playerControls.Disable();
    }
}