using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private Vector3 mousePos;

    

    [SerializeField] private Transform gunCenter;
    [SerializeField] private Animator gunAnimator;
    
    void Start()
    {
       
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            gunAnimator.SetBool("StillShooting", true);
        }
        else
        {
            gunAnimator.SetBool("StillShooting", false);
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - gunCenter.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        gunCenter.rotation = Quaternion.Euler(0f,0f,angle);
    }

}
