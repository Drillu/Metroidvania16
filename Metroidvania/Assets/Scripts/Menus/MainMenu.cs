using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;


    public void PlayButton()
    {
        Debug.Log("Play Button CLicked");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void QuitButton()
    {
        Debug.Log("Quit Button CLicked");
        Application.Quit();
    }
    IEnumerator LoadScene(int sceneIndex)
    {
        animator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
