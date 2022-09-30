using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private float despawn = 50f;

    private void Awake()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/WaterGunShoot");
    }

    private void Update()
    {
        despawn -= 0.1f;
        if (despawn <= 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Damage player
            //Destroy animation

            //Destroy bullet
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            //Destroy animation

            //Destroy bullet
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Quail") || collision.CompareTag("SludgeBunny"))
        {
            Destroy(gameObject);
        }
    }
}
