using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Transform cameraTransform;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 playerVelocity;
    private bool isGrounded;
    private Vector2 moveInput;
    private float turnSmoothVelocity;

    [SerializeField] private AnimatorHandler animatorHandler;

    private Animator animator;

    private void OnEnable()
    {
        var playerInput = new PlayerControls();
        playerInput.Enable();
        playerInput.PlayerMovement.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.PlayerMovement.Movement.canceled += ctx => moveInput = Vector2.zero;
        playerInput.PlayerMovement.Jump.performed += ctx => Jump();

        cameraTransform = Camera.main.transform;

        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        // Update animator parameters
        animator.SetFloat("Horizontal", direction.magnitude >= 0.1f ? Mathf.Abs(moveInput.x) : 0);
        animator.SetFloat("Vertical", direction.magnitude >= 0.1f ? Mathf.Abs(moveInput.y) : 0);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
