using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    public LayerMask enemiesLayerMask;
    private float maxHealth;
    private float currHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthSlider.maxValue;
        currHealth = maxHealth;
    }

    private void Update()
    {
        healthSlider.value = currHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (GeneralManager.CollisionIsTouchingLayer(other,enemiesLayerMask))
        {
            Debug.Log("bruh");
            currHealth = HealthManager.TakeDamageImmediate(currHealth, 4f);
        }
    }
}
