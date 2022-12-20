using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    // animations
    internal enum LadybugAnimation {
            fly,
            shoot
    }

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

            // Handle animations
            HandleAnimations();
        }
    }

    //////////////////////
    // Functions//
    //////////////////////

    override protected void HandleAnimations() {
        // Handles animations
        if      (currentAnimation == (int)LadybugAnimation.fly){
            animator.Play("Ladybug_fly");

            shotChargeTimePassed = 0.0f;
            animator.speed = 1.0f;
        }
        else if (currentAnimation == (int)LadybugAnimation.shoot){

            animator.Play("Ladybug_shoot");

            float shotAnimationTime = 1.51f; // accourding to the time of "Ladybug_shoot" animation
            float speedRate = shotAnimationTime/shotChargeTimeDelay;
            animator.speed = speedRate;
            Debug.Log("Animation time: " + shotAnimationTime);
        }

    }

    // Hovers in the air
    private void Hover(){ rb.AddForce(new Vector2(0, Mathf.Cos(Time.time))*2); }

    override public void Follow(GameObject target){
        // Moves the ladybug towards the target
        Vector2 newTargetPosition = target.transform.position + new Vector3(0, shootDistance/1.8f, 0); // actually follows above the target
        if (Helper.GetLinearVelocity(rb) <= baseSpeed){
            Helper.PushTowards2D(rb, this.transform.position, newTargetPosition, baseSpeed/5);
        } 
        currentAnimation = (int)LadybugAnimation.fly;
    } 
    // checks the charge status
    private bool IsShotCharged(){ return (shotChargeTimePassed >= shotChargeTimeDelay); }

    private void Shoot(){
        // shoot a projectile
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

    }
    private void ShootOnFrame(){
        // shoots a projectile on the correct frame
        int correctFrame = 9;
        int currentFrame = -1;

        string name = sprite.sprite.name; // current sprite name
        if (name.Contains("shoot")){
            // get current frame
            while (name.Contains("_")) name = name.Remove(0, 1);
            currentFrame = int.Parse(name);

            // shoot when it's time
            if (currentFrame == correctFrame){
                Shoot();
                shotChargeTimePassed = 0.0f; // reset timer
            }
        }
    }
    override public void Attack(){
        // Attack
        if (IsShotCharged()){
            ShootOnFrame();
        } else {
            // sync charge animation
            currentAnimation = (int)LadybugAnimation.shoot;
            // shot not charged, charge shot
            shotChargeTimePassed += Time.deltaTime; 
        }
    }
}