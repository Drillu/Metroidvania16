using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static float TakeDamageImmediate(float currentHP, float damage)
    {
         return currentHP - damage;
    }
}
