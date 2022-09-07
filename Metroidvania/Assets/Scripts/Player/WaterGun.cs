using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private Vector3 mousePos;

    [SerializeField] private Transform gunCenter;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private SpriteRenderer gunSprite;

    FMOD.Studio.EventInstance waterEvent;
    const string sprayingSound = "event:/SFX/WaterGun";

    private void Awake()
    {
        waterEvent = FMODUnity.RuntimeManager.CreateInstance(sprayingSound);
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
        if (gunCenter.rotation.eulerAngles.z > 90f && gunCenter.rotation.eulerAngles.z < 270f)
        {
            gunCenter.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            gunCenter.localScale = new Vector3(1, 1, 1);
        }
            

        if (Input.GetMouseButtonDown(0)) // First press
        {

            StartCoroutine(StartSpraying());
            waterEvent.start();

          
        }

        

        else if (Input.GetMouseButtonUp(0)) // release
        { 
            StopAllCoroutines();
            gunAnimator.Play("waterSprayEmpty");
            waterEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
          
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - gunCenter.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        gunCenter.rotation = Quaternion.Euler(0f,0f,angle);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("shooting enenmy");
        }
    }

    private IEnumerator StartSpraying()
    {
        gunAnimator.Play("waterSprayStart");
        yield return new WaitForSeconds(1f);
        gunAnimator.Play("waterSprayContinuous");
    }
}
