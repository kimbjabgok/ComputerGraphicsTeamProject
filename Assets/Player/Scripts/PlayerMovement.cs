using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Camera playerCam;
    private float xRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;

        Vector3 moveDir = camForward.normalized * moveZ + camRight.normalized * moveX;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 velocity = moveDir * currentSpeed;
        Vector3 yVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        rb.linearVelocity = velocity + yVelocity;
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}