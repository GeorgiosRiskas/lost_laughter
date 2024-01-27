using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    PlayerControls inputActions;
    CameraHandler cameraHandler;

    Vector2 movementInput;
    Vector2 cameraInput;

    private bool cameraCanRotate;

    private void Awake()
    {
        cameraHandler = CameraHandler.singleton;

        EventsManager.OnDialogueStartedEvent += EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent += EventsManager_OnDialogueEndedEvent;
    }

    private void OnDestroy()
    {
        EventsManager.OnDialogueStartedEvent -= EventsManager_OnDialogueStartedEvent;
        EventsManager.OnDialogueEndedEvent -= EventsManager_OnDialogueEndedEvent;
    }

    private void EventsManager_OnDialogueStartedEvent(string dialogue, NPC activeNpc)
    {
        cameraCanRotate = false;
    }

    private void EventsManager_OnDialogueEndedEvent()
    {
        cameraCanRotate = true;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            if (cameraCanRotate)
            {
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }
    }

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            // No jump-related code in this version
        }

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
}
