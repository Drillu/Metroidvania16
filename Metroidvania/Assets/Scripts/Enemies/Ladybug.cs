using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ladybug : Enemy
{

    // SerializeField
        // movement
    [SerializeField] private float baseSpeed = 10.0f;
        // projectiles
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 20.0f;
    [SerializeField] private float projectileExistenceTime = 5.0f;
    [SerializeField] private float shotChargeTimeDelay = 2.0f;
    [SerializeField] private float shootDistance = 20.0f;


    private float speedMultiplier = 1.0f;

    // projectiles
    private float shotChargeTimePassed = 0.0f;
    internal Vector3 projectileSpawnPosition; // initial local spawn position for the projectile spawn

    
    // before first frame
    new void Start(){
        base.Start();

        projectileSpawnPosition = this.GetComponentsInChildren<Transform>()[1].localPosition;
    }

    // called every frame
    void Update(){
        if (isActive && target != null){

            rb.velocity *= 0.992f; // always calm the velocity haha

            // when close to target, attack.
            // when not, move towards it (when in detection range);
            float targetDistance = DistanceToTarget();
            if (targetDistance <= shootDistance){ Hover(); Attack(); }
            else if (targetDistance <= targetDetectionDistance) Follow(target);

            // Face the target and handle repositioning the projectile spawn position
            FaceTarget();
            this.GetComponentsInChildren<Transform>()[1].localPosition = new Vector3(
                projectileSpawnPosition.x*((sprite.flipX)?-1.0f:1.0f),
                projectileSpawnPosition.y,
                projectileSpawnPosition.z
            );
        }
    }

//////////////////////
        // Functions//
//////////////////////
    
    // Hovers in the air
    private void Hover(){
        rb.AddForce(new Vector2(0, Mathf.Cos(Time.time))*2);
    }

    // Moves the ladybug towards the target
    override public void Follow(GameObject target){
        Vector2 newTargetPosition = target.transform.position + new Vector3(0, shootDistance/1.3f, 0); // actually follows above the target
        if (Helper.GetLinearVelocity(rb) <= baseSpeed){
            Helper.PushTowards2D(rb, this.transform.position, newTargetPosition, baseSpeed/5);
        } 
        animator.Play("Ladybug_fly");
    } 
    // checks the charge status
    private bool IsShotCharged(){ return (shotChargeTimePassed >= shotChargeTimeDelay); }

    // Attack
    override public void Attack(){
        if (IsShotCharged()){

            // shoot projectile
            Vector3 spawnPos = GetComponentsInChildren<Transform>()[1].position;
            GameObject p = Instantiate(projectile, spawnPos, Quaternion.identity);
            // point it to target and shoot
            Helper.PushTowards2D(
                p.GetComponent<Rigidbody2D>(),
                p.transform.position,
                target.transform.position,
                projectileSpeed
            );
            // destruction properties
            Destroy(p, projectileExistenceTime);

            shotChargeTimePassed = 0.0f; // reset timer
            return;
        }
        
        shotChargeTimePassed += Time.deltaTime; // shot not charged, charge shot
    }
}