using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaterGun : MonoBehaviour
{
    private Vector3 mousePos;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunCenter;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private float fireRate = 25f;
    Vector2 lookDir;
    Vector2 shotDir;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        if (gunCenter.rotation.eulerAngles.z > 90f && gunCenter.rotation.eulerAngles.z < 270f)
        {
            gunCenter.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            gunCenter.localScale = new Vector3(1, 1, 1);
        }
            
        if (Input.GetMouseButtonDown(0)) // First press
        {
            StartCoroutine(StartSpraying());
        }

        if(Input.GetMouseButton(0)) //Hold
        {
            if (fireRate <= 0)
            {
                fireRate = 25f;
                Shoot();
            }
            else
                fireRate -= 0.1f;
        }

        else if (Input.GetMouseButtonUp(0)) // release
        { 
            StopAllCoroutines();
            gunAnimator.Play("waterSprayEmpty");
        }
    }

    void FixedUpdate()
    {
        lookDir = mousePos - gunCenter.position;
        shotDir = firePoint.position - gunCenter.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        gunCenter.rotation = Quaternion.Euler(0f,0f,angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SludgeBunny"))
        {
            
            Debug.Log("shooting bunny, playing sound");
        }

        else if (collision.CompareTag("Quail"))
        {
            Debug.Log("shooting quail, playing sound");
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(shotDir * 4f, ForceMode2D.Impulse);
    }

    private IEnumerator StartSpraying()
    {
        gunAnimator.Play("waterSprayStart");
        yield return new WaitForSeconds(1f);
        gunAnimator.Play("waterSprayContinuous");
    }
}
