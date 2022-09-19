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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentHP -= 2f;
        }
    }
    
}
