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
    internal enum AnimationState { 
        IDLE,
        SKATING,
        JUMPING,
        FALLING,
        GLIDING
}

    // SerializeField
    [SerializeField] private bool controllable = true;
    [SerializeField] private bool playAnimations = true;
    [SerializeField] private bool ableToDash = true;
    [SerializeField] private float dashDelaySeconds = 0.2f;
    [SerializeField] private float dashDurationSeconds = 0.5f; // How many seconds to hold the dash for
    [SerializeField] private float dashPower = 20.0f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private MovementType movementType = MovementType.SKATE;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private Weapon weapon = Weapon.NONE;

    // Movement properties
    private Rigidbody2D rb;
    private Collider2D footCollider;

        // inputs
    internal float directionX = 0.0f; // horizontal input
    internal bool jump = false;

        // dash
    private float dashDelay = 0;
    private float dashDurationStart;
    private float dashDurationEnd;
    private Vector3 dashStartPos;
    private bool dashingRight = true;
    private Vector3 lastDashPosition; // if dash was interupted, the location to set to (to avoid dash glitches)


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
        if (movementType == MovementType.SKATE)
            rb.AddForce(HandleSkatingMovement());
    }
    void Update()
    {
        // Called once per frame

        float directionX = Input.GetAxis("Horizontal");
        if (controllable) HandleMovementInputs();
        if (playAnimations) HandleAnimations();
    }

//////////////////////////////
        // Custom Functions///
//////////////////////////////
    public bool IsOnGround(){
        return footCollider.IsTouchingLayers(jumpableGround);
    }
    private void HandleMovementInputs(){
        // handles movement inputs etc...

        directionX = Input.GetAxis("Horizontal"); // Horizontal input
        jump = Input.GetButton("Jump"); // is "jump" pressed this frame?
        // dash
        if (dashDelay > 0) dashDelay -= Time.deltaTime;
        if (ableToDash && !IsOnGround() && Input.GetButton("Dash") && dashDelay <= 0.0f){
            TriggerDash();
        }
    }
    private void HandleAnimations(){

        AnimationState animationState = AnimationState.IDLE;
        float directionX = Input.GetAxis("Horizontal");
        animator.speed = 1.0f;

        // When skating
        if (movementType == MovementType.SKATE){
            if (IsOnGround()){ // ground
                if (directionX == 0){ // no horizotal input
                    if (Mathf.Abs(rb.velocity.x) < movementSpeed/10.0f) // speed less than 10% of move speed, stand.
                        animationState = AnimationState.IDLE;
                    else // if not, then glide.
                        animationState = AnimationState.GLIDING;
                } else{ // has input, play skate animation
                    animationState = AnimationState.SKATING;
                    animator.speed = 2.0f;
                }
            } else{ // not on ground
                if (rb.velocity.y > 0) // going up
                    animationState = AnimationState.JUMPING;
                else
                    animationState = AnimationState.FALLING;
            }
        }

        // handle flipping
        if (directionX < 0)                     spriteRenderer.flipX = false; 
        else if (directionX > 0)                spriteRenderer.flipX = true;

        
        animator.SetInteger("State", (int)animationState);
    }
    private bool IsOnSkatePushFrame(){
        // Used when the movement type is "SKATE"
        // Checks if on a valid skate frame (pushing)

        string spritename = spriteRenderer.sprite.name;

        if (!spritename.Contains("skating"))
            return false; // maybe not(?)

        string spritenumber = spritename.Replace("Kirei_skating_spritesheet_", string.Empty);
        int currentFrame = int.Parse(spritenumber);

        int[] validFrames = {0, 6}; // valid frames
        foreach (int frame in validFrames)
            if (frame == currentFrame) return true;
        
        // not a valid frame
        return false;
    }

    // Movement Handling
    private Vector2 HandleSkatingMovement(){
        // returns a 2d vector of force added
        
        float velX = 0; // return value (x)
        float velY = 0; // return value (y)

        if (IsOnGround()){
            // Horizontal movement
            if (IsOnSkatePushFrame() && Mathf.Abs(rb.velocity.x) < movementSpeed*3.0f) velX += directionX * movementSpeed;

            //// old jump mechanic
            // Jump if pressed and calculate jump height accourding to speed
            //if (jump) velY = jumpHeightMin + ((jumpHeightMax-jumpHeightMin)*(Mathf.Abs(rb.velocity.x)/maxMoveSpeed));

            // new jump "mechanic"
            if (jump) velY = jumpForce;

        } else { // not on ground
            float regularXVelocity = movementSpeed * 4.0f; // appearently the velocity is around 4 times as much as "movementSpeed"
            if (Mathf.Abs(rb.velocity.x) < regularXVelocity) velX += directionX * (regularXVelocity/3.0f); // when mid-air, horizontal speed is a third of regular.
        }

        // dash
        if (IsCurrentlyDashing() && !IsOnGround())
            Dash();
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // importent in order to free the player from the dash


        return new Vector2(velX, velY);
    }

    // Dashing
    private void TriggerDash(){
        // basically triggers the dash and sets the dash variables
        dashDelay = dashDelaySeconds;
        dashDurationStart = Time.time;
        dashDurationEnd = dashDurationStart + dashDurationSeconds;
        dashStartPos = this.transform.position;
        dashingRight = spriteRenderer.flipX;
   }
    private void Dash(){
        rb.constraints = RigidbodyConstraints2D.FreezePositionY ^ RigidbodyConstraints2D.FreezeRotation; // Added bitwise OR to  prevent rotation
        rb.velocity = new Vector2(0, 0);
        this.transform.position += new Vector3(dashPower*(dashingRight?1.0f:-1.0f), 0, 0);
        lastDashPosition = this.transform.position;
   }
    public bool IsCurrentlyDashing(){ return Time.time < dashDurationEnd; }
    public float GetDashDelayTime() {return dashDelaySeconds;}
}
