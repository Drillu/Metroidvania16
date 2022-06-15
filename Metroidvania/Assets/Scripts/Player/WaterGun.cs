using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private Vector2 mousePos;

    

    [SerializeField] private Transform gunCenter;
    [SerializeField] private Animator gunAnimator;
    
    void Start()
    {
       
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 180f;
        gunCenter.rotation = Quaternion.Euler(0f,0f,angle);
    }

}
