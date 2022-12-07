using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for the dash charge bar
public class DashChargeBar : MonoBehaviour
{
    // SerializeField
    [SerializeField] private NewPlayerMovement target;

    private float maximumSize; // when charge is 100%

    // Start is called before the first frame update
    void Start()
    {
        maximumSize = this.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null){
            float max = target.GetDashDelayTime();
            float current = target.dashDelay;
            this.transform.localScale = new Vector3(((max-current)/max)*maximumSize, this.transform.localScale.y, this.transform.localScale.z);

        }else{Debug.Log(this.name + " has no target!");}
    }
}
