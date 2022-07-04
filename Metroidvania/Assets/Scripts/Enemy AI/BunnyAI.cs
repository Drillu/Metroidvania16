using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAI : MonoBehaviour
{
    #region Movement Variables
    [SerializeField] private float movementSpeed = 4f;
    private Rigidbody2D rb;
    private Vector2 movement = Vector2.zero;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform edgeCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool wallBool;
    public bool edgeBool;
    #endregion

    private GameObject player;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool stillPassive = true;
    [SerializeField] private float timeRequiered = 80f;
    private float timer = 80f;
    private bool isAbleToJump = true;
    private bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement.x = movementSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //Detection range
        if(Vector2.Distance(player.transform.position,transform.position)<=8f)
        {
            stillPassive = false;
        }

        if(stillPassive)
        {
            wallBool = Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
            edgeBool = Physics2D.OverlapCircle(edgeCheck.position, 0.1f, groundLayer);
            if (!edgeBool || wallBool)
                Flip();
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        else
        {

            if (player.transform.position.x > transform.position.x && !isFacingRight)
                Flip();
            else if (player.transform.position.x < transform.position.x && isFacingRight)
                Flip();

            if (timer<=0)
            {
                if(isAbleToJump)
                {
                    //Jump
                    rb.AddForce(new Vector2(movementSpeed,jumpForce), ForceMode2D.Impulse);
                    isAbleToJump = false;
                }
                

            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                //Go timer down
                timer -= 0.2f;
            }
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            isAbleToJump = true;
            timer = timeRequiered;
            
        }
    }

    void Flip()
    {
        Debug.Log("Flip!");
        movementSpeed *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180f, 0));
    }
    
}
