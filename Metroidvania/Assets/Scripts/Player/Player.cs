using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum MovementType{
        SKATE,
        WALK,
        RUN,
        FLY
    }
    private enum Weapon{
        NONE,
        WATERGUN
    }
    internal enum MovementState { 
        IDLE,
        SKATING,
        JUMPING,
        FALLING,
        GLIDING
}

    // SerializeField
    [SerializeField] private bool controllable = true;
    [SerializeField] private bool playAnimations = true;
    [SerializeField] private MovementType movementType = MovementType.SKATE;
    [SerializeField] private Weapon weapon = Weapon.NONE;

    private Rigidbody2D rb; // Player's rigidbody
    private Collider2D footCollider;

    // inputs
    private float directionX = 0.0f; // Used for checking the horizontal input
    internal bool jump = false;

    // animation stuff
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    void Start()
    {
        // before the first frame

        rb = GetComponent<Rigidbody2D>();
        footCollider = GetComponentsInChildren<Collider2D>()[1];
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastDashPosition = this.transform.position;

    }
    void FixedUpdate()
    {
        // Called every fixed framerate frame
        rb.AddForce(HandleSkatingMovement());
    }
    void Update()
    {
        // Called once per frame

        float directionX = Input.GetAxis("Horizontal");
        if (controllable) HandleInputs();
        if (playAnimations) HandleAnimations();
    }

//////////////////////////////
        // Custom Functions///
//////////////////////////////
    private void HandleAnimations(){
        // Sets the state of the animator to the appropriate animation

        MovementState state;
        float directionX = Input.GetAxis("Horizontal");
        animator.speed = 1.0f;

        if (IsOnGround()){ // ground
            if (directionX == 0){ // no horizotal input
                if (Mathf.Abs(rb.velocity.x) < maxMoveSpeed/10.0f) // speed less than 10% of move speed, stand.
                    state = MovementState.IDLE;
                else if (movementType == MovementType.SKATE) // if not, then glide (only when skating)
                    state = MovementState.GLIDING;
            } else{ // has input, play animation
                if (movementType == MovementType.SKATE){
                    MovementState.SKATING;
                    animator.speed = 2.0f;
                }
            }
        } else{ // not on ground
            if (rb.velocity.y > 0) // going up
                state = MovementState.JUMPING;
            else
                state = MovementState.FALLING;
        }
        // handle flipping
        if (directionX < 0)                     spriteRenderer.flipX = false; 
        else if (directionX > 0)                spriteRenderer.flipX = true;

        
        animator.SetInteger("State", (int)state);

    }
    private Vector2 HandleSkatingMovement(){
        // returns a 2d vector of force added
        
        float velX = 0; // return value (x)
        float velY = 0; // return value (y)

        if (IsOnGround()){
            // Horizontal movement
            if (IsOnSkatePushFrame() && Mathf.Abs(rb.velocity.x) < maxMoveSpeed) velX += directionX * moveSpeed;

            // Jump if pressed and calculate jump height accourding to speed
            if (jump) velY = jumpHeightMin + ((jumpHeightMax-jumpHeightMin)*(Mathf.Abs(rb.velocity.x)/maxMoveSpeed));

        } else velX += airMoveSpeed * directionX;

        // dash
        if (IsCurrentlyDashing() && !IsOnGround())
            Dash();
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // importent in order to free the player from the dash


        return new Vector2(velX, velY);
    }
}
