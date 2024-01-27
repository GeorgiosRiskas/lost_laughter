using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    private LayerMask ignoreLayers;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public static CameraHandler singleton;

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot = -35;
    public float maximumPivot = 35;

    private void Awake()
    {
        // Singleton pattern
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Debug.LogWarning("Duplicate CameraHandler instance detected. Destroying the new one.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        myTransform = transform;
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        defaultPosition = cameraTransform.localPosition.z;
        //Related to camera colliding with enviroment
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(
            myTransform.position,
            targetTransform.position,
            ref cameraFollowVelocity,
            delta / followSpeed
        );
        myTransform.position = targetPosition;
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (mouseYInput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        rotation.x = pivotAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        //cameraPivotTransform.localRotation = targetRotation;
        myTransform.rotation = targetRotation;
    }

}
