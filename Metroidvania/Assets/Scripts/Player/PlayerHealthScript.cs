using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public Image healthBarImage;
    public LayerMask enemiesLayerMask;
    public List<Sprite> healthStates;
    private int maxHealth = 5;
    private int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (GeneralManager.CollisionIsTouchingLayer(other,enemiesLayerMask))
        {
            Debug.Log("Player Takes damage");
            currHealth--;
            healthBarImage.sprite = healthStates[currHealth]; //Error right now cuz the game doesn't end
        }
    }
}
