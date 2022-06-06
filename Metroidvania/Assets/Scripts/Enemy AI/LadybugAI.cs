using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadybugAI : MonoBehaviour
{
    private GameObject player;
    private bool inChase = false;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed=2f;
    [SerializeField] private float fireRate=30f;
    [SerializeField] private float distanceRequieredToTriggerChase = 12f;

    public GameObject bulletPrefab;
    public Transform Center;
    public Transform firePoint;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 lookDir = player.transform.position - Center.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Center.rotation = Quaternion.Euler(0f, 0f, angle);

        if (inChase)
        {
            if(fireRate>0)
            {
                fireRate -= 0.2f;
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(lookDir * 3f, ForceMode2D.Impulse);
                fireRate = 30f;
            }
            return;
        }

        if(Vector2.Distance(transform.position,player.transform.position)<=distanceRequieredToTriggerChase)
        {
            inChase = true;
        }
    }

    private void FixedUpdate()
    {
        if (inChase)
        {
            rb.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
