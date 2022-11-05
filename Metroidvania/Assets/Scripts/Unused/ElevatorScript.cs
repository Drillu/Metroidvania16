using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private float elevationSpeed=5f;
    private Rigidbody2D rb;
    private float dir = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0f, elevationSpeed * dir); 
    }



}
