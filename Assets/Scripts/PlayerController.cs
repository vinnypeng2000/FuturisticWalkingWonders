using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private float verticalLookRotation;
    private float horizontalLookRotation;

    public CinemachineVirtualCamera cinemachineCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent rigidbody from rotating due to physics

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Jump();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player left and right
        transform.Rotate(Vector3.up * mouseX);
        // cinemachineCamera.transform.Rotate(Vector3.up * mouseX);

        // Adjust the POV (pitch) directly through the Cinemachine's POV component
        CinemachinePOV pov = cinemachineCamera.GetCinemachineComponent<CinemachinePOV>();
        if (pov != null)
        {
            pov.m_HorizontalAxis.Value += mouseX;
            pov.m_VerticalAxis.Value -= mouseY;
        }

        // // Rotate the camera up and down
        // verticalLookRotation -= mouseY;
        // verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f); // Clamp the rotation so you don't rotate too far
        // Camera.main.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        // horizontalLookRotation -= mouseX;
        // horizontalLookRotation = Mathf.Clamp(horizontalLookRotation, -90f, 90f); // Clamp the rotation so you don't rotate too far
        // Camera.main.transform.localRotation = Quaternion.Euler(horizontalLookRotation, 0f, 0f);
    }

    void Move()
    {
        // Get movement input
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        // Get the forward and right directions relative to the camera
        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;

        // Calculate the movement direction relative to the player's forward direction
        Vector3 move = (playerRight * moveX + playerForward * moveZ).normalized;

        // Apply the movement to the Rigidbody
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);


        // Vector3 cameraForward = cinemachineCamera.transform.forward;
        // Vector3 cameraRight = cinemachineCamera.transform.right;

        // // Project forward and right on the horizontal plane (ignore y)
        // cameraForward.y = 0f;
        // cameraRight.y = 0f;

        // cameraForward.Normalize();
        // cameraRight.Normalize();

        // // Calculate the movement direction relative to the camera's view
        // Vector3 move = (cameraRight * moveX + cameraForward * moveZ).normalized;

        // // Apply the movement to the Rigidbody
        // rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        // Check if the player is on the ground by casting a ray downwards
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
