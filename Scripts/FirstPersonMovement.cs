using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float movementSmoothTime = 0.1f; // Adjust for smoother movement
    public float rotationSmoothTime = 0.05f; // Adjust for smoother rotation
    public Camera playerCamera; // Assign this in the Inspector

    private float xRotation = 0f;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseVelocity = Vector2.zero;
    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            Debug.LogWarning("Player Camera is not assigned!");
        }
    }

    void Update()
    {
        RotateView();
        MovePlayer();
    }

    private void RotateView()
    {
        if (playerCamera == null) return;

        // Smooth mouse input
        float targetMouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float targetMouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentMouseDelta.x = Mathf.SmoothDamp(currentMouseDelta.x, targetMouseX, ref currentMouseVelocity.x, rotationSmoothTime);
        currentMouseDelta.y = Mathf.SmoothDamp(currentMouseDelta.y, targetMouseY, ref currentMouseVelocity.y, rotationSmoothTime);

        // Vertical rotation with clamping
        xRotation -= currentMouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera and player body smoothly
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * currentMouseDelta.x);
    }

    private void MovePlayer()
    {
        // Get smooth movement input along local X and Z axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;
        Vector3 targetVelocity = direction * moveSpeed;
        Vector3 smoothedVelocity = Vector3.SmoothDamp(controller.velocity, targetVelocity, ref currentVelocity, movementSmoothTime);

        // Move the player
        controller.Move(smoothedVelocity * Time.deltaTime);
    }
}
