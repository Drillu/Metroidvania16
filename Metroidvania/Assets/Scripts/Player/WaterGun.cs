using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterGun : MonoBehaviour
{
    private Vector3 mousePos;

    // SerializeField
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float fireForce = 1000.0f;
    Vector2 lookDir;
    Vector2 shotDir;

    private float radius = 8.0f;
    private float shotSpeedCounter = 0;

    void Start()
    {

    }
    private void Update()
    { 
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       

        

        if(Input.GetMouseButton(0)) //Hold
        {
            // only be able to shoot if the fire rate interval is reached
            if (shotSpeedCounter >= fireRate) 
            {
                shotSpeedCounter = 0; // reset counter
                Shoot();
            }
        }

        
    }

    void FixedUpdate()
    {
        // rotation
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        this.transform.rotation = Quaternion.Euler(0f,0f,angle);

        // position
        if (player != null){
            this.transform.position = player.transform.position;

            // place watergun on a cirlce around the player
            this.transform.position -= new Vector3(
                Mathf.Cos(Helper.DegreesToRads(angle)) * radius, // x
                Mathf.Sin(Helper.DegreesToRads(angle)) * radius, // y
                0
            );
        }

        lookDir = mousePos - this.transform.position;
        shotDir = this.transform.position - this.transform.position;

        // fire rate counter stuff
        if (shotSpeedCounter < fireRate) shotSpeedCounter += Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        //bullet.GetComponent<Rigidbody2D>().AddForce(shotDir * 4f, ForceMode2D.Impulse);
        Helper.PushTowards2D(
            bullet.GetComponent<Rigidbody2D>(),
            bullet.transform.position,
            mousePos,
            fireForce
        );
    }

    
}
