using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    [HideInInspector] public static LayerMask enemyLayerMask;
    [HideInInspector] public static LayerMask groundLayerMask;

    private void Start()
    {
        enemyLayerMask = LayerMask.GetMask("Enemies");
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    public static bool TriggerIsTouchingLayer(Collider2D collision, LayerMask givenLayer)
    {
        if (((1 << collision.gameObject.layer) & givenLayer) != 0)
            return true;
        else
            return false;
    }

    public static bool CollisionIsTouchingLayer(Collision2D collision, LayerMask givenLayer)
    {
        if (((1 << collision.gameObject.layer) & givenLayer) != 0)
            return true;
        else
            return false;
    }

}
