using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ladybug : Enemy
{

    // SerializeField
        // movement
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float baseSpeed;

    new void Start(){
        base.Start();
    }
    void Update(){
        if (isActive && target != null){
            Follow(target);
        }
    }
    
    // Moves the ladybug towards the target
    override public void Follow(GameObject target){
        // can follow?
        float distance = DistanceToTarget();
        if (distance > detectionDistance) return;
        // speed multiplier
        speedMultiplier = 3 * (distance/detectionDistance);
        // add a force to the velocity accourding to target's position and base speed times multiplier
        Vector2 wantedPos = Vector2.MoveTowards(this.transform.position, target.transform.position, baseSpeed * speedMultiplier);
        Vector2 force = new Vector2(wantedPos.x - this.transform.position.x, wantedPos.y - this.transform.position.y);
        rb.AddForce(force*speedMultiplier);
        // set animation speed to suit the force
        animator.speed = speedMultiplier;
        animator.Play("Ladybug_fly");
        // Flip sprite
        sprite.flipX = this.transform.position.x - target.transform.position.x <= 0;

    } 
    override public void Attack(){
    }
}