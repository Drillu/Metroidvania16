using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a class for various functions that might help
static class Helper
{
    
    /// <summary>
    /// Pushes a rigidbody to a target position
    /// </summary>
    public static void PushTowards2D(Rigidbody2D rb, Vector2 originPos, Vector2 targetPos, float force){
        Vector2 wantedPos = Vector2.MoveTowards(originPos, targetPos, force);
        Vector2 addForce = new Vector2(wantedPos.x - originPos.x, wantedPos.y - originPos.y);
        rb.AddForce(addForce * force);
    }
    /// <summary>
    /// Gets a rigidbody's current linear velocity
    /// </summary>
    public static float GetLinearVelocity(Rigidbody2D rb){
        float aSq = Mathf.Pow(rb.velocity.x, 2);
        float bSq = Mathf.Pow(rb.velocity.y, 2);
        float velocity = Mathf.Sqrt(aSq + bSq);
        return velocity;
    }
}