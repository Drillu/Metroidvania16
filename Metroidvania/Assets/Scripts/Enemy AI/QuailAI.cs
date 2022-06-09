using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuailAI : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4f;
    private Rigidbody2D rb;
    private Vector2 movement = Vector2.zero;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform edgeCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool wallBool;
    public bool edgeBool;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement.x = movementSpeed;

    }

    private void FixedUpdate()
    {
        wallBool = Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
        edgeBool = Physics2D.OverlapCircle(edgeCheck.position, 0.1f, groundLayer);
        if (!edgeBool || wallBool)
            Flip();
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    void Flip()
    {
        Debug.Log("Flip!");
        movementSpeed *= -1;
        transform.Rotate(new Vector3(0, 180f, 0));
    }


}
