using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float currentHP;
    [SerializeField] private float maxHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            currentHP--;
            if(currentHP==0)
            {
                if (gameObject.CompareTag("Quail"))
                    GetComponent<QuailAI>().Die();
                else if (gameObject.CompareTag("SludgeBunny"))
                    GetComponent<BunnyAI>().Die();
            }
        }
    }
}
