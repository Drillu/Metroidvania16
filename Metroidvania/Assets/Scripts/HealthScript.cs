using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private float currentHP;
    [SerializeField] private float maxHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("HIT ENEMY");
            currentHP -= 1f;
        }
        if(currentHP<=0)
        {
            Destroy(gameObject);
        }
    }
    
}
