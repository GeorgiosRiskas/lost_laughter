using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float rotationSpeed = 5.0f;

    private PlayerControls controls; // Reference to the new input system controls
    private Vector2 rotationInput;
    private float pitch = 0.0f;
    private float yaw = 0.0f;

    private bool cameraCanRotate = true;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.PlayerMovement.Camera.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
        controls.PlayerMovement.Camera.canceled += ctx => rotationInput = Vector2.zero;

        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void EventsManager_OnDialogueEndedEvent()
    {
        cameraCanRotate = true;
    }

    private void EventsManager_OnDialogueStartedEvent(string dialogue, NPC activeNpc)
    {
        cameraCanRotate = false;
    }

    private void OnEnable()
    {
        controls.Enable();
        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent += EventsManager_OnDialogueEndedEvent;
    }

    private void OnDisable()
    {
        controls.Disable();
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent -= EventsManager_OnDialogueEndedEvent;
    }

    void Update()
    {
        if (cameraCanRotate)
        {
            float mouseX = rotationInput.x / Screen.width;
            float mouseY = rotationInput.y / Screen.height;

            yaw += rotationSpeed * mouseX * Time.deltaTime;
            pitch -= rotationSpeed * mouseY * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, -10, 35); // Limit the pitch rotation
        }
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
