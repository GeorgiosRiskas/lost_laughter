using UnityEngine;

public class CharacterController : MonoBehaviour
{
 private Rigidbody rb;
    public float jumpForce = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnJumpInput()
    {
        Jump();
    }

    private void Jump()
    {
        // Implement your jump logic here
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // Implement your ground detection logic here
        // Return true if grounded, false otherwise
        return true;
    }
}
