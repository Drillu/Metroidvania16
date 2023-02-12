using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for the dash charge bar
public class DashChargeBar : MonoBehaviour
{
    // SerializeField
    [SerializeField] private NewPlayerMovement target;
    [SerializeField] private Vector2 offsets = new Vector2(0, 0);
        // bobbing
    [SerializeField] private bool alwaysShow = false;
    [SerializeField] private bool bobbing;
    [SerializeField] private float bobbingAmount;
    [SerializeField] private float deadzoneRadius = 10.0f;
    [SerializeField] private float maxDistance = 20.0f;

    private Transform chargeTransform;
    private float maximumSize; // when charge is 100%

    // Bobbing effect
    private Vector3 centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        chargeTransform = GetComponentsInChildren<Transform>()[1];
        maximumSize = chargeTransform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null){

            // Dash charge
            float max = target.GetDashDelayTime();
            float current = target.dashDelay;
            chargeTransform.localScale = new Vector3(((max-current)/max)*maximumSize, this.transform.localScale.y, this.transform.localScale.z);

            // if not always shown to the player
            // hide when unable to dash, show when able.
            if (!alwaysShow && (target.IsOnGround() || (max-current) >= max))
                this.transform.localScale = Vector3.zero;
            else
                this.transform.localScale = Vector3.one;

            // Bobbing stuff
            if (bobbing) Bob();

        }else{Debug.Log(this.name + " has no target!");}

    }
    

/////////////////
    // Functions/
/////////////////

    private void Bob(){
        Rigidbody2D targetRB = target.GetComponent<Rigidbody2D>();
        centerPoint = target.transform.position +
            (Vector3.right * offsets.x) + // x offset
            (Vector3.up * offsets.y);     // y offset

        // move towards wanted position
        Vector3 curPos = this.transform.position;
        Vector3 addPos = new Vector3(0, 0, 0);
        float distance = Helper.Distance2D((Vector2)this.transform.position, (Vector2)centerPoint);
        if (deadzoneRadius < distance){
            float power = bobbingAmount * (distance / maxDistance);
            if      (centerPoint.x < curPos.x)   addPos += Vector3.left * power;
            else if (centerPoint.x > curPos.x)   addPos += Vector3.right * power;
            if      (centerPoint.y < curPos.y)   addPos += Vector3.down * power;
            else if (centerPoint.y > curPos.y)   addPos += Vector3.up * power;
        }

        // apply
        this.transform.position += (Vector3)addPos;
    }
}
