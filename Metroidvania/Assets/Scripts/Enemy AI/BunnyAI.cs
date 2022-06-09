using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAI : MonoBehaviour
{
    private GameObject player;
    private bool isTrigger = false;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleTrigger;
    private float currentChargeTime;

    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float maxChargeTime = 60f;

    #region Unity Functions
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        capsuleTrigger = GetComponent<CapsuleCollider2D>();
        currentChargeTime = maxChargeTime;
    }

    private void FixedUpdate()
    {
        /*
         * Passive(when isTriggered = false) behavior:
         *      - WalkAroundPlatform()
         *      - FlipOnEdge()
         *      
         */
        if(!isTrigger)
        {
            WalkArondPlatform();
            FlipOnEdge();
        }
        /*
         * Triggered behavior:
         *      - FlipNecessary()
         *      - StartTimer()
         *      - Jump and Land (OnTrigger Events)
         *      - Reset timer & isGrounded=true
         */
        else
        {
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            isGrounded = true;
            currentChargeTime = maxChargeTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }
    }
    #endregion

    #region Made Functions
    void WalkArondPlatform()
    {

    }

    void FlipOnEdge()
    {

    }
    #endregion
}
