using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] //Always privatize variables and use SerializeField to have it still function as public for Unity
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private Collider2D playerGroundCollider;

    private bool isOnGround;
    private float horizontalMovement;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private bool isFacingRight = true;
    private int Jumps = 0;

    public static bool canDoubleJump = false;
    void Update ()
    {
        UpdateIsOnGround();
        HandleHorizontalInput();
        HandleJumpInput();
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        if(canDoubleJump && isOnGround)
        {
            Jumps = 2;
        }
    }
    private void HandleHorizontalInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && (isOnGround || Jumps > 0))
        {
            myRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if(Jumps > 0)
            {
                Jumps--;
            }
        }
    }
    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
    }
    private void UpdatePhysicsMaterial()
    {
        if (horizontalMovement == 0)
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
    }
    private void Move()
    {
        myRigidbody.AddForce(Vector2.right * horizontalMovement * accelerationForce);
        Vector2 clampedVelocity = myRigidbody.velocity;
        clampedVelocity.x = Mathf.Clamp(myRigidbody.velocity.x, -maxSpeed, maxSpeed);
        myRigidbody.velocity = clampedVelocity;
    }
}
