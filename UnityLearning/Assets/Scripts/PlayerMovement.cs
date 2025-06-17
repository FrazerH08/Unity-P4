using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpkey = KeyCode.Space;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientaion;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation =true; 
        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down , playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else{
            rb.linearDamping = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    } 

    private void FixedUpdate()
    {
        MovePlayer();
    } 
    private void MyInput () 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump =false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientaion.forward * verticalInput + orientaion.right * horizontalInput;

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up *jumpForce, ForceMode.Impulse);
    }
    private void  ResetJump()
    {
        readyToJump = true;
    }

        private void FireBullet()
    {
        GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.linearVelocity = this.transform.forward * bulletSpeed;  // Use velocity instead of linearVelocity
    }
}
