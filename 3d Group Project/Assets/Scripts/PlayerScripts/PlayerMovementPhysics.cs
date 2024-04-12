using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementPhysics : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float airMultiplier = 0.5f;
    public float groundDrag = 5f;

    public Transform orientation;

    float horizontalInput, verticalInput;

    Rigidbody rb;

    Vector3 moveDirection;
    Vector2 look;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        UpdateMovement();
        UpdateLook();
        SpeedControl();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight);

            if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
    }
    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }

    void UpdateMovement()
    {
       horizontalInput  = Input.GetAxisRaw("Horizontal");
       verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        { rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); }

        else if (!grounded)
        { rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); }
    }

    void SpeedControl()
    {
      Vector3 FlatVel = new Vector3(rb.velocity.x, 0f,rb.velocity.z);
        if (FlatVel.magnitude > moveSpeed)
        {
            Invoke(nameof(SpeedControlPartTwo), 0.3f);
        }
    }
    void SpeedControlPartTwo()
    {
        Vector3 FlatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (FlatVel.magnitude > moveSpeed && gameObject.GetComponent<DashScript>().isDashing == false)
        {
            Vector3 limitedVel = FlatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpSpeed * 10f, ForceMode.Impulse);
        
    }
}
