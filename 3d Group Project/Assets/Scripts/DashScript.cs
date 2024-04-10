using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce, dashUpwardForce, dashDuration;
    public bool isDashing = false;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.Mouse2;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey))
            Dash();
    }

    private void Dash()
    {

        Vector3 forceToApply = orientation.forward * dashForce * 100f + orientation.up * dashUpwardForce * 10f;

        rb.AddForce(forceToApply, ForceMode.Impulse);

        isDashing= true;

        Invoke(nameof(ResetDash),dashDuration);
    }
    private void ResetDash()
    {
        isDashing = false;
    }
}
