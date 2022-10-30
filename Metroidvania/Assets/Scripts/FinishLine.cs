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
            AudioManager.CutMusic();
            PlayerMovement.ShutDownSounds();
        }
    }
}
