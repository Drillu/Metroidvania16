using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField] internal int buttonValue;
    [SerializeField] internal ElevatorScript elevatorScript;
    bool move;

    private void Update()
    {
        move = Input.GetKeyDown(KeyCode.Keypad2);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(move)
            {
                if (buttonValue == 1)
                    elevatorScript.GoUp();
                else
                    elevatorScript.GoDown();
            }
        }
    }

}
