using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraRoot;

    [Header("Sensitivity")]
    [SerializeField] private float mouseMultiplier = 0.1f;

    [SerializeField] private float gamepadMultiplier = 120f;

    [Header("Look Settings")]
    [SerializeField] private float maxLookAngle = 85f;

    private IInputService inputService;

    private ISaveService saveService;

    private float pitch;

    private float sensitivity;

    private void Start()
    {
        inputService =
            ServiceLocator.Get<IInputService>();

        saveService =
            ServiceLocator.Get<ISaveService>();

        sensitivity =
            saveService.Data.sensitivity;

        if (sensitivity <= 0f)
        {
            sensitivity = 1f;
        }
    }

    private void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        Vector2 lookInput =
            inputService.Look;

        if (inputService.IsGamepad)
        {
            lookInput.x =
                ApplyGamepadCurve(
                    lookInput.x);

            lookInput.y =
                ApplyGamepadCurve(
                    lookInput.y);
        }

        float finalSensitivity =
            inputService.IsGamepad
                ? sensitivity *
                  gamepadMultiplier
                : sensitivity *
                  mouseMultiplier;

        float deltaMultiplier =
            inputService.IsGamepad
                ? Time.deltaTime
                : 1f;

        float yaw =
            lookInput.x *
            finalSensitivity *
            deltaMultiplier;

        float pitchInput =
            lookInput.y *
            finalSensitivity *
            deltaMultiplier;

        pitch -= pitchInput;

        pitch = Mathf.Clamp(
            pitch,
            -maxLookAngle,
            maxLookAngle);

        cameraRoot.localRotation =
            Quaternion.Euler(
                pitch,
                0f,
                0f);

        transform.Rotate(
            Vector3.up * yaw);
    }

    private float ApplyGamepadCurve(
        float value)
    {
        float abs =
            Mathf.Abs(value);

        float curved =
            abs * abs;

        return curved *
               Mathf.Sign(value);
    }

    public void SetSensitivity(
        float value)
    {
        sensitivity = value;

        saveService.Data.sensitivity =
            value;

        saveService.Save();

       
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }
}