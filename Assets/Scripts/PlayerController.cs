using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity;
    public float jumpForce = 5f;

    private Rigidbody rb;

    public Camera camera;
    public CharacterController characterController;
    float xRotation = 0f;
    float speed = 12f;

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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime   ;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Get the forward and right direction relative to the camera
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        Debug.Log(cameraForward);
        // Flatten the forward and right directions to the horizontal plane
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize the vectors
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction based on the input and camera direction
        Vector3 moveDirection = transform.forward  * moveZ + transform.right * moveX; 
        characterController.Move(moveDirection * speed * Time.deltaTime);
        
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