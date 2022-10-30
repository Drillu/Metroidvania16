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
    [SerializeField] private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        //playerSprite = GetComponent<SpriteRenderer>();
        currHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (GeneralManager.CollisionIsTouchingLayer(other,enemiesLayerMask))
       {
            TakeDamage();
       }
    }

    private void TakeDamage()
    {
        Debug.Log("Player Takes damage");
        currHealth--;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KireiHurt");
        StartCoroutine(ChangeSpriteToRed());
        healthBarImage.sprite = healthStates[currHealth]; //Error right now cuz the game doesn't end
    }

    IEnumerator ChangeSpriteToRed()
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = Color.white;
    }
}
