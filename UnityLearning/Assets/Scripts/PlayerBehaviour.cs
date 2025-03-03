using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    private float vInput;
    private float hInput;

    private Rigidbody _rb;
    private CapsuleCollider _col;
    private GameBehaviour _gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gather input
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        // Handle shooting
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        // Jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        // Apply forward/backward movement (using transform.forward)
        Vector3 forwardMovement = transform.forward * vInput * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + forwardMovement);

        // Apply left/right movement (using transform.right)
        if (hInput != 0f)
        {
            // Rotate the player based on the horizontal input
            float rotationAmount = hInput * Time.fixedDeltaTime;
            Quaternion targetRotation = Quaternion.Euler(0, rotationAmount, 0);
            _rb.MoveRotation(_rb.rotation * targetRotation);  // Smoothly rotate the player
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }

    private void FireBullet()
    {
        GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
        Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
        bulletRB.linearVelocity = this.transform.forward * bulletSpeed;  // Use velocity instead of linearVelocity
    }
}
