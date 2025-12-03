using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumps = 2;
    
    private Animator animator;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded;
    private int layerGround = 3;
    private int layerIsland = 7;
    private int layerLeftWall = 8;
    private int layerRightWall = 9;
    private bool touchingLeftWall;
    private bool touchingRightWall;
    private float moveX;
    
    InputAction moveAction;
    InputAction jumpAction;


    //Animation Methods
    void UpdateAnimations()
    {
        walkingAnimation(moveX, isGrounded);
        jumpingAnimation(isGrounded, rb.linearVelocity.y);
        fallingAnimation(isGrounded, rb.linearVelocity.y);
        landingAnimation(isGrounded, rb.linearVelocity.y);

    }   

    // Walking vs Idle
    public void walkingAnimation(float moveX, bool isGrounded)
    {
        bool walking = Mathf.Abs(moveX) > 0.1f && isGrounded;
        animator.SetBool("isWalking", walking);
        animator.SetBool("isIdle", !walking && isGrounded);
    }

    // Jumping
    public void jumpingAnimation(bool isGrounded, float yVelocity)
    {
        bool jumping = !isGrounded && yVelocity > 0;
        animator.SetBool("isJumping", jumping);
    }

    // Falling
    public void fallingAnimation(bool isGrounded, float yVelocity)
    {
        bool falling = !isGrounded && yVelocity < 0;
        animator.SetBool("isFalling", falling);
    }

    // Landing
    public void landingAnimation(bool isGrounded, float yVelocity)
    {
        bool landing = isGrounded && yVelocity <= 0;
        animator.SetBool("isLanding", landing);
    }
    public void isTouchingGround()
    {
        if(rb.IsTouchingLayers(1 << layerGround))
        {
            isGrounded = true;
            jumpCount = 0;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {   
        isTouchingGround();
        UpdateAnimations();
        moveX = InputSystem.actions["Move"].ReadValue<Vector2>().x;
        //Checks if player is touching a leftWall
        if(rb.IsTouchingLayers(1 << layerLeftWall))
        {
            touchingLeftWall = true;
        }
        else
        {
            touchingLeftWall = false;
        }
        //Checks if player is touching a leftWall
        if(rb.IsTouchingLayers(1 << layerRightWall))
        {
            touchingRightWall = true;
        }
        else
        {
            touchingRightWall = false;
        }
        //If player is touching left wall and moving right Block Movement
        if (touchingLeftWall&& moveX > 0)
        {
           moveX = 0; 
        }
        //If player is touching right wall and moving left Block Movement
        if (touchingRightWall && moveX < 0)
        {
           moveX = 0; 
        }
        //Sets player movement direction based on input recorded in moveX
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        //Checks if player can Double Jump
        if (jumpAction.triggered)
        {
            if(isGrounded)
            {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("jumpTrigger");
            jumpCount++; 
            }  
            else if(jumpCount < maxJumps)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("doubleJumpTrigger");
                jumpCount++;
            }
        }
    }
}
