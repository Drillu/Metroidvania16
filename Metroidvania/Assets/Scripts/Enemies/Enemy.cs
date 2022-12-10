using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A general class for enemies
abstract class Enemy : MonoBehaviour
{
    // SerializeField
    [SerializeField] protected GameObject target;
    [SerializeField] protected float detectionDistance;
    [SerializeField] protected bool isActive = true;

    public float hp;
    protected Rigidbody2D rb;

    // animations
    protected Animator animator;
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
    // Movement
    abstract public void Follow(GameObject target);
    // Combat
    abstract public void Attack();
    
}
