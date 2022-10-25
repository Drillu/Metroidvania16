using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == ("Player"))
        {
            SceneManager.LoadScene("Main Menu");
            AudioManager.gameMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            AudioManager.gameAmbiance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        
    }
}
