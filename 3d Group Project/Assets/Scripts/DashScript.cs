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
    public float maxSpeed;
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
        MaxSpeed();

        if (Input.GetKeyDown(dashKey))
            Dash();

        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;


        rb.velocity = new Vector3(0f, 0f,0f);
        Vector3 forceToApply = orientation.forward * dashForce * 100f + orientation.up * dashUpwardForce * 10f;

        rb.AddForce(forceToApply, ForceMode.Impulse);

        isDashing= true;

        Invoke(nameof(ResetDash),dashDuration);
    }
    private void ResetDash()
    {
        isDashing = false;
    }

    private void MaxSpeed()
    {
       
        Vector3 FlatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (FlatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = FlatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    
    }
}
