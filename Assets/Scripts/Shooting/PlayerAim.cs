using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("FOV")]
    [SerializeField] private float normalFOV = 90f;

    [SerializeField] private float aimFOV = 60f;

    [SerializeField] private float smoothSpeed = 10f;

    private IInputService inputService;

    private float targetFOV;

    private void Awake()
    {
        inputService =
            ServiceLocator.Get<IInputService>();

        targetFOV =
            normalFOV;
    }

    private void OnEnable()
    {
        if (inputService != null)
        {
            inputService.OnAimChanged +=
                HandleAimChanged;
        }
    }

    private void OnDisable()
    {
        if (inputService != null)
        {
            inputService.OnAimChanged -=
                HandleAimChanged;
        }
    }

    private void Update()
    {
        playerCamera.fieldOfView =
            Mathf.Lerp(
                playerCamera.fieldOfView,
                targetFOV,
                smoothSpeed * Time.deltaTime);
    }

    private void HandleAimChanged(
        bool aiming)
    {
        targetFOV =
            aiming
                ? aimFOV
                : normalFOV;
    }
}