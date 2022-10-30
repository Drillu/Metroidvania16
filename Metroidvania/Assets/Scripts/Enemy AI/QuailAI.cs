using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuailAI : MonoBehaviour
{
    #region Variables
    //Define at Start()
    private Rigidbody2D rb;
    private Vector2 movement = Vector2.zero;
    private HealthScript healthScript;
    //Serializable or private
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform edgeCheck;
    [SerializeField] private GameObject movingChild;
    [SerializeField] private GameObject ded;
    [SerializeField] private LayerMask groundLayer;
    private bool wallBool = false;
    private bool edgeBool = true;
    //Animation Variables
    private Animator animator;
    const string quail_idle = "Quail_idle_anim";
    const string quail_idle2 = "Quail_idle2_anim";
    const string quail_running = "Quail_running_anim";
    const string quail_death = "Quail_death_anim";
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement.x = movementSpeed;
        healthScript = GetComponent<HealthScript>();
        animator = GetComponent<Animator>();
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
        movementSpeed *= -1;
        transform.Rotate(new Vector3(0, 180f, 0));
    }

    public void Die()
    {
        movementSpeed = 0f;
        StopAllCoroutines();
        movingChild.SetActive(false);
        ded.SetActive(true);
        animator.Play(quail_death);
       
        Destroy(gameObject,6f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("HIT ENEMY");
            healthScript.currentHP -= 1f;
        }
    }

}
