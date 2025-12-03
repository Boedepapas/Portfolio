using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumps = 2;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded;
    private int layerGround = 3;
    private int layerLeftWall = 8;
    private int layerRightWall = 9;
    private bool touchingLeftWall;
    private bool touchingRightWall;
    private float moveX;
    private bool walking;
    
    InputAction moveAction;
    InputAction jumpAction;


    

    public void flipSprite(float moveX)
    {
        if(moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    
       
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        bool wasGrounded = isGrounded;
        isGrounded = rb.IsTouchingLayers(1 << layerGround);
        if (isGrounded && !wasGrounded) jumpCount = 0;

        moveX = InputSystem.actions["Move"].ReadValue<Vector2>().x;
        flipSprite(moveX);
        //Checks if player is touching a leftWall
        
        if(rb.IsTouchingLayers(1 << layerLeftWall))
        {
            touchingLeftWall = true;
            if(moveX > 0 && touchingLeftWall)
            {
               moveX = 0; 
            }
        }
        else
        {
            touchingLeftWall = false;
        }
        //Checks if player is touching a rightWall
        if(rb.IsTouchingLayers(1 << layerRightWall))
        {
           touchingRightWall = true;
            if(moveX < 0 && touchingRightWall)
            {
                moveX = 0;
            }
        }
        else
        {
            touchingRightWall = false;
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
        //Sets Animation States
        animator.SetBool("isIdle", isGrounded && Mathf.Abs(moveX) < 0.1f);
        animator.SetBool("isWalking", isGrounded && Mathf.Abs(moveX) >= 0.1f);
        animator.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < -0.1f);
        animator.SetBool("isLanding", isGrounded && !wasGrounded);

    }
}
