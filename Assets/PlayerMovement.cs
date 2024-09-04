using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;
    public float smoothTime;
    public float groundDrag;
    public float dashSpeed;
    public Transform orientation;

    public float dashCooldown;
    float dashCooldownTimer;

    float horizontalInput;
    float verticalInput;
    float normSpeed;

    bool sprint = false;

    private float smoothVelocity = 0f;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        normSpeed = moveSpeed;
        dashCooldownTimer = 0;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

       rb.drag = groundDrag;

        if(dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        Sprint();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            sprint = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            sprint = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if dash cooldown is 0 u can dash
            if(dashCooldownTimer <= 0)
                Dash();
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void Sprint() 
    {
        if (sprint)
        {
            moveSpeed = Mathf.SmoothDamp(moveSpeed, sprintSpeed, ref smoothVelocity, smoothTime);
        }
        else
        {
            moveSpeed = Mathf.SmoothDamp(moveSpeed, normSpeed, ref smoothVelocity, smoothTime);
        }
    }

    private void Dash() 
    {
        //dash
        rb.AddForce(moveDirection.normalized * dashSpeed * 20, ForceMode.Impulse);
        this.gameObject.tag = "Invulnerable";
        Invoke(nameof(TagToPlayer), 0.5f);
        //set dash cooldown
        dashCooldownTimer = dashCooldown;
    }

    private void TagToPlayer() 
    {
        this.gameObject.tag = "Player";
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
