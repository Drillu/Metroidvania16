using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    
    public LayerMask enemiesLayerMask;
    private float maxHealth = 5f;
    private float currHealth;

    // Start is called before the first frame update
    void Start()
    {
       
        currHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (GeneralManager.CollisionIsTouchingLayer(other,enemiesLayerMask))
        {
            Debug.Log("bruh");
            
        }
    }
}
