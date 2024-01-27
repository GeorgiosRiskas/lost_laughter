using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    InputHandler inputHandler;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animatorHandler;

    new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    PlayerControls inputActions;

    private bool jumpInput;

    bool playerIsJumping;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();
    }

    public float gravity;

    public void Update()
    {
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);

        // Get the horizontal forward and right vectors of the camera
        Vector3 forwardHorizontal = Vector3.ProjectOnPlane(cameraObject.forward, Vector3.up).normalized;
        Vector3 rightHorizontal = Vector3.ProjectOnPlane(cameraObject.right, Vector3.up).normalized;

        // Use the horizontal vectors for movement
        moveDirection = forwardHorizontal * inputHandler.vertical;
        moveDirection += rightHorizontal * inputHandler.horizontal;
        moveDirection.Normalize();

        float speed = movementSpeed;
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;
        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

        if (animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }


    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }

}
