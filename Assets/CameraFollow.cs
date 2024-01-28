using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 5.0f;

    private PlayerControls controls; // Reference to the new input system controls
    private Vector2 rotationInput;
    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.PlayerMovement.Camera.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Camera.canceled += ctx => rotationInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        yaw += rotationSpeed * rotationInput.x * Time.deltaTime;
        pitch -= rotationSpeed * rotationInput.y * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -35, 35); // Limit the pitch rotation
    }

    void LateUpdate()
    {
        // Apply rotation
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        // Calculate the new position based on player position and offset
        Vector3 offsetPosition = Quaternion.Euler(pitch, yaw, 0) * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, player.position + offsetPosition, smoothSpeed);

        // Update the position and look at the player
        transform.position = smoothedPosition;
        transform.LookAt(player.position);
    }
}