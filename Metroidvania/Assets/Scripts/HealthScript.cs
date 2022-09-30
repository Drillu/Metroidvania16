using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private float currentHP;
    [SerializeField] private float maxHP;
    [SerializeField] private AnimationClip deathAnimation;
    [SerializeField] private GameObject enableDeath;

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
            StartCoroutine(DeathAnimation());
        }
    }
    
    IEnumerator DeathAnimation()
    {
        GetComponent<Animator>().Play(deathAnimation.name);
        enableDeath.SetActive(true);
        Destroy(GetComponentInChildren<CapsuleCollider2D>());
        yield return new WaitForSeconds(deathAnimation.length);
        Destroy(gameObject);
    }
}
