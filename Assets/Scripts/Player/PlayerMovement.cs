using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float acceleration = 10f;
    
    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private IInputService inputService;
    private ICursorService cursorService;
    private Vector3 currentVelocity;
    private Vector3 velocitySmooth;

    private float verticalVelocity;

    private bool isGrounded;
    private bool isSprinting;

    private void Awake()
    {
        inputService = ServiceLocator.Get<IInputService>();
        cursorService = ServiceLocator.Get<ICursorService>();
        cursorService.SetGameplayMode();
    }

    private void OnEnable()
    {
        inputService.OnJump += Jump;
    }

    private void OnDisable()
    {
        inputService.OnJump -= Jump;
    }

    private void Update()
    {
        GroundCheck();

        HandleGravity();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = inputService.Move;

        Vector3 moveDirection =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        float targetSpeed =
            isSprinting ? sprintSpeed : walkSpeed;

        Vector3 targetVelocity =
            moveDirection * targetSpeed;

        currentVelocity = Vector3.SmoothDamp(
            currentVelocity,
            targetVelocity,
            ref velocitySmooth,
            1f / acceleration);

        Vector3 finalVelocity = new Vector3(
            currentVelocity.x,
            verticalVelocity,
            currentVelocity.z);

        characterController.Move(
            finalVelocity * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        verticalVelocity = Mathf.Max(verticalVelocity, -50f);
    }

    private void Jump()
    {
        if (!isGrounded)
            return;

        verticalVelocity =
            Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundMask);
    }

    public void SetSprint(bool value)
    {
        isSprinting = value;
    }
}