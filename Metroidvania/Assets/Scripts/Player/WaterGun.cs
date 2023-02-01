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
    Vector2 lookDir;
    Vector2 shotDir;

    private float radius = 8.0f;

    void Start()
    {

    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        // position
        if (player != null){
            this.transform.position = player.transform.position;
        }

        this.transform.position = player.transform.position;
        // place watergun on a cirlce around the player
        

        // rotation
        if (this.transform.rotation.eulerAngles.z > 90f && this.transform.rotation.eulerAngles.z < 270f) {
            this.transform.localScale = new Vector3(1, -1, 1);
        } else {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
            
     

        if(Input.GetMouseButton(0)) //Hold
        {
            // Keep shooting synchronized accourding to the fire
            if (fireRate <= 0)
            {
                fireRate = 15f;
                Shoot();
            }
            else
                fireRate -= 0.1f;
        }

        
    }

    void FixedUpdate()
    {
        lookDir = mousePos - this.transform.position;
        shotDir = this.transform.position - this.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        this.transform.rotation = Quaternion.Euler(0f,0f,angle);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(shotDir * 4f, ForceMode2D.Impulse);
    }

    
}
