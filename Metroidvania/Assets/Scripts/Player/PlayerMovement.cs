using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private Animator camBHV;

    //28.10.2022 Trying to put in skates sound
    static public FMOD.Studio.EventInstance skateRoadIns;
    //private FMOD.Studio.EventInstance skateCarIns;
    private bool isPlayingSkates = false;
    const string SkateRoad = "event:/SFX/SkateRoad";
    const string SkateCar = "event:/SFX/SkateCar";

    //Function reference to make the sprites change.
    private Animator anim;
    
    float directionX = 0f;


    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpHeight = 14f;

    private BoxCollider2D coll;

    internal enum MovementState { idle, running, jumping, falling, gliding }
   
    private SpriteRenderer sprite;
    private TrailRenderer _trailRenderer;
    [SerializeField] private float _dashingVelocity = 14f;
    [SerializeField] private float _dashingTime = 0.5f;
    private Vector2 _dashingDir;
    private bool _isDashing;
    private bool _canDash = true;

    #region Audio
    //Paths
    const string KireiJump = "event:/SFX/KireiJump";
    const string JumpRoad = "event:/SFX/JumpRoad";
    const string JumpCar = "event:/SFX/JumpCar";
    const string LandRoad = "event:/SFX/LandRoad";
    const string LandCar = "event:/SFX/LandCar";

    //Other


    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        _trailRenderer = GetComponent<TrailRenderer>();
        skateRoadIns = FMODUnity.RuntimeManager.CreateInstance(SkateRoad);
       // skateCarIns = FMODUnity.RuntimeManager.CreateInstance(SkateCar);
        skateRoadIns.setParameterByName("Move", 1f);
    }

    public static void ShutDownSounds()
    {
        skateRoadIns.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void Update()
    {
        directionX = Input.GetAxis("Horizontal");
        // 28.10.2022 Trying to put in skates sound

        if (directionX != 0f && !isPlayingSkates && isGrounded())
        {
            Debug.Log("Bruh");
            skateRoadIns.start();
            isPlayingSkates = true;
        }
        if (directionX == 0f && isPlayingSkates || !isGrounded())
        {
            skateRoadIns.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            isPlayingSkates = false;
        }
            
        //Changed GetAxisRaw into GetAxis to give that slippery acceleration movement - Ersan (09.06.2022)

        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
       
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            FMODUnity.RuntimeManager.PlayOneShot(KireiJump);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        
        UpdateAnimations();
        
        var dashInput = Input.GetButtonDown("Dash");
        
        if (dashInput && _canDash)
        {
            _isDashing = true;
            _canDash = false;
            _trailRenderer.emitting = true;
            _dashingDir = new Vector2(directionX, 0);
            // changed Input.GetAxisRaw("Vertical") to 0 so that player can only dash horizontally 
            if (_dashingDir == Vector2.zero)
            {
                _dashingDir = new Vector2(transform.localScale.x, 0);
            }
            rb.gravityScale = 0;
            StartCoroutine(StopDashing());

        }
        //This part is hopefully gonna get used when we make an animation state for dashing - Ersan (08.06.2022)
        //anim.SetBool("IsDashing", _isDashing);

        if (_isDashing)
        {
            rb.velocity = _dashingDir.normalized * _dashingVelocity;
            return;
        }

        if (isGrounded())
        {
            _canDash = true;
        }
        //Uncomment the part below if they want the player to not be able to dash mid air

        //else
        //{
        //    _canDash = false;
        //}
    }


    private void UpdateAnimations()
    {
        MovementState state;

        if (directionX > 0f)
        {

            state = MovementState.running;

            sprite.flipX = true;
        }
        else if (directionX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;

        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        
        }

        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("State", (int)state);

    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _isDashing = false;
        rb.gravityScale = 1;
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Road"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(LandRoad);
            camBHV.Play("ZoomOut");
            
        }
        else if(other.CompareTag("Car"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(LandCar);
            camBHV.Play("ZoomOut");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Road"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(JumpRoad);
            camBHV.Play("ZoomIn");
        }
        else if(other.CompareTag("Car"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(JumpCar);
            camBHV.Play("ZoomIn");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(GeneralManager.CollisionIsTouchingLayer(other,enemyLayerMask))
        {
            Debug.Log("Enemy COllided");
        }
    }
}



// Things that did not work as intended
//isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.9f, 0.2f), 0f, jumpableGround);
//public bool isGrounded;