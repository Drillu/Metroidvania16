using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    // SerializeField
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private bool controllable = true;
    [SerializeField] private bool playAnimations = true;
        // movement
    [SerializeField] private float moveSpeed = 15.0f;
    [SerializeField] private float maxMoveSpeed = 15.0f;
    [SerializeField] private float jumpHeightMax = 40.0f;
    [SerializeField] private float jumpHeightMin = 15.0f;
    [SerializeField] private float airMoveSpeed = 10.0f;
        // dash
    [SerializeField] private bool dashEnabled = true;
    [SerializeField] private float dashChargeSeconds = 0.2f;
    [SerializeField] private float dashTimeSeconds = 0.5f; // How many seconds to hold the dash for
    [SerializeField] private float dashPower = 20.0f;

    // Movement properties
    private Rigidbody2D rb;
    private Collider2D footCollider;

        // inputs
    internal float directionX = 0.0f;
    internal bool jump = false;

    // dash
    private float dashDelay = 0;
    private float dashTimeStart;
    private Vector3 dashStartPos;

    // animation
    internal enum MovementState { idle, running, jumping, falling, gliding }
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footCollider = GetComponentInChildren<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        rb.AddForce(HandleMovement());
    }

    // Update is called once per frame
    void Update()
    {
        float directionX = Input.GetAxis("Horizontal");
        if (controllable) HandleInputs();
        if (playAnimations) HandleAnimations();
    }


///////////////////////
        // Functions///
///////////////////////

    private bool IsOnGround(){
        return footCollider.IsTouchingLayers(jumpableGround);
    }

    // Checks if on a valid skate frame (pushing)
    private bool IsOnSkatePushFrame(){
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

    // handle movement inputs etc...
    private void HandleInputs(){
        directionX = Input.GetAxis("Horizontal"); // Horizontal input
        jump = Input.GetButton("Jump"); // is "jump" pressed this frame?
        if (Input.GetButton("Dash") && dashDelay <= 0){ // dash
            dashDelay = dashChargeSeconds; 

            dashTimeStart = Time.time;
            dashStartPos = this.transform.position;
        }
    }

    // returns a 2d vector of force added
    private Vector2 HandleMovement(){
        float velX = 0; // return value (x)
        float velY = 0; // return value (y)

        if (IsOnGround()){
            // Horizontal movement
            if (IsOnSkatePushFrame() && Mathf.Abs(rb.velocity.x) < maxMoveSpeed) velX += directionX * moveSpeed;

            // Jump if pressed and calculate jump height accourding to speed
            if (jump) velY = jumpHeightMin + ((jumpHeightMax-jumpHeightMin)*(Mathf.Abs(rb.velocity.x)/maxMoveSpeed));

        } else{
            if (Mathf.Abs(rb.velocity.x) < maxMoveSpeed) velX += airMoveSpeed * directionX;
        }

        // dash
        float dashEndTime = dashTimeStart + dashTimeSeconds;
        if (dashTimeStart + (Time.time-dashTimeStart) < dashEndTime){
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.velocity = new Vector2(0, 0);
            Dash();
        } else{
            rb.constraints = RigidbodyConstraints2D.None;
        }



        return new Vector2(velX, velY);
    }

    private void HandleAnimations(){

        MovementState state;
        float directionX = Input.GetAxis("Horizontal");

        if (IsOnGround()){ // ground
            if (directionX == 0){ // no horizotal input
                if (Mathf.Abs(rb.velocity.x) < maxMoveSpeed/10.0f) // speed less than 10% of move speed, stand.
                    state = MovementState.idle;
                else // if not, then glide.
                    state = MovementState.gliding;
            } else{ // has input, play skate animation
                state = MovementState.running;
            }
        } else{ // not on ground
            if (rb.velocity.y > 0) // going up
                state = MovementState.jumping;
            else
                state = MovementState.falling;
        }
        // handle flipping
        if (directionX < 0)                     spriteRenderer.flipX = false; 
        else if (directionX > 0)                spriteRenderer.flipX = true;

        
        animator.SetInteger("State", (int)state);

    }

    // dash
    private void Dash(){
        rb.velocity.Set(dashPower, 0);
        dashDelay -= Time.deltaTime;
   }
}
