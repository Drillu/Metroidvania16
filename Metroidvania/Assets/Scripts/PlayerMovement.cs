using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    //Function reference to make the sprites change.
    private Animator anim;

    float directionX = 0f;


    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpHeight = 14f;

    private BoxCollider2D coll;

    private enum MovementState { idle, running, jumping, falling }

    private SpriteRenderer sprite;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
     
        directionX = Input.GetAxisRaw("Horizontal");

        if ((this.name == "PlayerTest"))
        {
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);


            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                //jumpingSoundFX.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
            //UpdateAnimations();
        }
    }

    //UpdateAnimations function is unused for now

    //private void UpdateAnimations()
    //{
    //    MovementState state;

    //    if (directionX > 0f)
    //    {

    //        state = MovementState.running;

    //        sprite.flipX = false;
    //    }
    //    else if (directionX < 0f)
    //    {
    //        state = MovementState.running;
    //        sprite.flipX = true;

    //    }
    //    else
    //    {
    //        state = MovementState.idle;
    //    }

    //    if (rb.velocity.y > .1f)
    //    {
    //        state = MovementState.jumping;
    //    }

    //    else if (rb.velocity.y < -.1f)
    //    {
    //        state = MovementState.falling;
    //    }

    //    anim.SetInteger("State", (int)state);

    //}

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
