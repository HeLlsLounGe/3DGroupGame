using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float Mass = 1f;
    [SerializeField] float mouseSensitivity = 3f;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        UpdateMovement();
        UpdateLook();
        UpdateGravity();

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
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
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        //transform.Translate(input * moveSpeed * Time.deltaTime, Space.World);
        controller.Move((input * moveSpeed + velocity) * Time.deltaTime);
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * Mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -2f : velocity.y + gravity.y;
    }
}
