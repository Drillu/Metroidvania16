using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private Vector3 mousePos;

    [SerializeField] private Transform gunCenter;
    [SerializeField] private PolygonCollider2D coll;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private SpriteRenderer gunSprite;

    FMOD.Studio.EventInstance waterEvent;
    const string sprayingSound = "event:/SFX/WaterGunOld";

    private void Awake()
    {
        waterEvent = FMODUnity.RuntimeManager.CreateInstance(sprayingSound);
        coll = GetComponent<PolygonCollider2D>();
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
            coll.enabled = true;
          
        }

        else if (Input.GetMouseButtonUp(0)) // release
        { 
            StopAllCoroutines();
            gunAnimator.Play("waterSprayEmpty");
            waterEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            coll.enabled = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - gunCenter.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
        gunCenter.rotation = Quaternion.Euler(0f,0f,angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SludgeBunny"))
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/BunnyHurt");
            Debug.Log("shooting bunny, playing sound");
        }

        else if (collision.CompareTag("Quail"))
        {
            Debug.Log("shooting quail, playing sound");
        }
    }

    private IEnumerator StartSpraying()
    {
        gunAnimator.Play("waterSprayStart");
        yield return new WaitForSeconds(1f);
        gunAnimator.Play("waterSprayContinuous");
    }
}
