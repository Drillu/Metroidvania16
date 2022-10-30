using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private float despawn = 80f;

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

            //Destroy animation

            
        }
        else if (GeneralManager.TriggerIsTouchingLayer(collision,GeneralManager.groundLayerMask))
        {
            //Destroy animation
            Debug.Log("General manager G layers work");


        }
        else if(GeneralManager.TriggerIsTouchingLayer(collision,GeneralManager.enemyLayerMask))
        {
            Debug.Log("General manager E layers work");
        }
        Destroy(gameObject);
    }
}
