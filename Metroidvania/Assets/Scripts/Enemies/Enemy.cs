using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A general class for enemies
abstract class Enemy : MonoBehaviour
{
    // SerializeField
    [SerializeField] protected GameObject target;
    [SerializeField] protected float targetDetectionDistance;
    [SerializeField] protected bool isActive = true;

    public float hp;
    protected Rigidbody2D rb;

    // animations
    protected Animator animator;
    protected int currentAnimation;
    protected SpriteRenderer sprite;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


//////////////////////
        // Functions//
//////////////////////

    // gets distance to target
    protected float DistanceToTarget(){
        Vector3 curPos = this.transform.position;
        Vector3 targetPos = target.transform.position;
        float a = Mathf.Max(curPos.x, targetPos.x) - Mathf.Min(curPos.x, targetPos.x);
        float b = Mathf.Max(curPos.y, targetPos.y) - Mathf.Min(curPos.y, targetPos.y);
        float distance = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
        return distance;
    }
    // Face target (x)
    protected void FaceTarget(){
        Vector2 curPos = this.transform.position;
        Vector2 targetPos = target.transform.position;
        sprite.flipX = (curPos.x < targetPos.x);

        Debug.Log("Flip state: " + sprite.flipX);
    }
    // Movement
    abstract public void Follow(GameObject target);
    // Combat
    abstract public void Attack();
    // Animation
    abstract protected void HandleAnimations();
    
}
