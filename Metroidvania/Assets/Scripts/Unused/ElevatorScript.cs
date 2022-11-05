using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    [SerializeField] private float elevationSpeed=8f;
    [SerializeField] private int currentDirection = -1;
    [SerializeField] private Transform currentDestination;
    [SerializeField] private Transform upperDestination;
    [SerializeField] private Transform lowerDestination;

    

    private Rigidbody2D rb;
    private bool needsToMove = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(needsToMove && Vector2.Distance(gameObject.transform.position,currentDestination.position) <= 0.3f)
        {
            rb.velocity = Vector2.zero;
            needsToMove = false;
        }
    }

    private void FixedUpdate()
    {
        if(needsToMove)
            rb.velocity = new Vector2(0f, elevationSpeed * currentDirection);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Keypad0))
                GoDown();
            else if (Input.GetKeyDown(KeyCode.Keypad1))
                GoUp();
        }
    }

    public void GoUp()
    {
        needsToMove = true;
        currentDestination = upperDestination;
        currentDirection = 1;
    }

    public void GoDown()
    {
        needsToMove = true;
        currentDestination = lowerDestination;
        currentDirection = -1;
    }

}
