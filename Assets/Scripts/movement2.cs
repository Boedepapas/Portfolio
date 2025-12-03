using UnityEngine;
using UnityEngine.InputSystem;

public class movement2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumps = 2;

    private Animator animator;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded;
    private float moveX;

    InputAction moveAction;
    InputAction jumpAction;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Reference your Input Actions properly
        var playerInput = GetComponent<PlayerInput>();
        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];

    }

    void Update()
    {
        // Ground check
        isGrounded = rb.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isGrounded) jumpCount = 0;

        // Movement
        moveX = moveAction.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Jump
        if (jumpAction.triggered)
        {
            if (isGrounded || jumpCount < maxJumps)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
                animator.SetTrigger(jumpCount == 1 ? "jumpTrigger" : "doubleJumpTrigger");
            }
        }

        // Animations
        animator.SetBool("isWalking", Mathf.Abs(moveX) > 0.1f && isGrounded);
        animator.SetBool("isIdle", Mathf.Abs(moveX) <= 0.1f && isGrounded);
        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0);
        animator.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < 0);
        animator.SetBool("isLanding", isGrounded && rb.linearVelocity.y <= 0);
    }
}