using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemService :
    MonoBehaviour,
    IInputService
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Action Maps")]
    [SerializeField] private string gameplayMap = "Gameplay";

    [SerializeField] private string uiMap = "UI";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction pauseAction;
    private InputAction reloadAction;
    private InputAction aimAction;

    private InputAction nextWeaponAction;
    private InputAction previousWeaponAction;

    private string currentControlScheme;

    private bool isAiming;

    public Vector2 Move =>
        moveAction.ReadValue<Vector2>();

    public Vector2 Look =>
        lookAction.ReadValue<Vector2>();

    public bool IsGamepad =>
        currentControlScheme == "Gamepad";

    public bool IsAiming =>
        isAiming;

    public bool ShootHeld =>
        shootAction != null &&
        shootAction.IsPressed();

    public event Action OnJump;
    public event Action OnShoot;
    public event Action OnPause;
    public event Action OnReload;

    public event Action OnNextWeapon;
    public event Action OnPreviousWeapon;

    public event Action<bool>
        OnAimChanged;

    public event Action<bool>
        OnInputSchemeChanged;

    private void Awake()
    {
        ServiceLocator.Register<IInputService>(this);

        InitializeActions();
    }

    private void Start()
    {
        currentControlScheme =
            playerInput.currentControlScheme;

        playerInput.onControlsChanged +=
            OnControlsChanged;
    }

    private void OnEnable()
    {
        SubscribeActions();
    }

    private void OnDisable()
    {
        UnsubscribeActions();

        playerInput.onControlsChanged -=
            OnControlsChanged;
    }

    private void InitializeActions()
    {
        moveAction =
            playerInput.actions["Move"];

        lookAction =
            playerInput.actions["Look"];

        jumpAction =
            playerInput.actions["Jump"];

        shootAction =
            playerInput.actions["Attack"];

        pauseAction =
            playerInput.actions["Pause"];

        reloadAction =
            playerInput.actions["Reload"];

        aimAction =
            playerInput.actions["Aim"];

        nextWeaponAction =
            playerInput.actions["NextWeapon"];

        previousWeaponAction =
            playerInput.actions["PreviousWeapon"];
    }

    private void SubscribeActions()
    {
        if (jumpAction != null)
        {
            jumpAction.performed +=
                OnJumpPerformed;
        }

        if (shootAction != null)
        {
            shootAction.performed +=
                OnShootPerformed;
        }

        if (pauseAction != null)
        {
            pauseAction.performed +=
                OnPausePerformed;
        }

        if (reloadAction != null)
        {
            reloadAction.performed +=
                OnReloadPerformed;
        }

        if (aimAction != null)
        {
            aimAction.performed +=
                OnAimStarted;

            aimAction.canceled +=
                OnAimCanceled;
        }

        if (nextWeaponAction != null)
        {
            nextWeaponAction.performed +=
                OnNextWeaponPerformed;
        }

        if (previousWeaponAction != null)
        {
            previousWeaponAction.performed +=
                OnPreviousWeaponPerformed;
        }
    }

    private void UnsubscribeActions()
    {
        if (jumpAction != null)
        {
            jumpAction.performed -=
                OnJumpPerformed;
        }

        if (shootAction != null)
        {
            shootAction.performed -=
                OnShootPerformed;
        }

        if (pauseAction != null)
        {
            pauseAction.performed -=
                OnPausePerformed;
        }

        if (reloadAction != null)
        {
            reloadAction.performed -=
                OnReloadPerformed;
        }

        if (aimAction != null)
        {
            aimAction.performed -=
                OnAimStarted;

            aimAction.canceled -=
                OnAimCanceled;
        }

        if (nextWeaponAction != null)
        {
            nextWeaponAction.performed -=
                OnNextWeaponPerformed;
        }

        if (previousWeaponAction != null)
        {
            previousWeaponAction.performed -=
                OnPreviousWeaponPerformed;
        }
    }

    private void OnControlsChanged(
        PlayerInput input)
    {
        if (currentControlScheme ==
            input.currentControlScheme)
        {
            return;
        }

        currentControlScheme =
            input.currentControlScheme;

        bool isGamepad =
            currentControlScheme == "Gamepad";

        OnInputSchemeChanged?.Invoke(
            isGamepad);

        Debug.Log(
            $"Input Scheme Changed: {currentControlScheme}");
    }

    private void OnJumpPerformed(
        InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void OnShootPerformed(
        InputAction.CallbackContext context)
    {
        OnShoot?.Invoke();
    }

    private void OnPausePerformed(
        InputAction.CallbackContext context)
    {
        OnPause?.Invoke();
    }

    private void OnReloadPerformed(
        InputAction.CallbackContext context)
    {
        OnReload?.Invoke();
    }

    private void OnAimStarted(
        InputAction.CallbackContext context)
    {
        isAiming = true;

        OnAimChanged?.Invoke(true);
    }

    private void OnAimCanceled(
        InputAction.CallbackContext context)
    {
        isAiming = false;

        OnAimChanged?.Invoke(false);
    }

    private void OnNextWeaponPerformed(
        InputAction.CallbackContext context)
    {
        OnNextWeapon?.Invoke();
    }

    private void OnPreviousWeaponPerformed(
        InputAction.CallbackContext context)
    {
        OnPreviousWeapon?.Invoke();
    }

    public void EnableGameplayInput()
    {
        playerInput.SwitchCurrentActionMap(
            gameplayMap);
    }

    public void EnableUIInput()
    {
        playerInput.SwitchCurrentActionMap(
            uiMap);
    }
}