using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Animator creditsAnimator;
    public CanvasGroup creditsGroup;

    const string Fade_End = "Fade_End";
    const string Fade_Start = "Fade_Start";
    const string Credits_Roll = "Credits_Roll";
    const string Credits_Back = "Credits_Back";

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

    public void PressedCredits()
    {
        StartCoroutine(LoadCredits());
    }

    public void BackFromCredits()
    {
        StartCoroutine(BackFromCreditsToMainMenu());
    }

    #region IEnumerators
    IEnumerator LoadScene(int sceneIndex)
    {
        animator.Play(Fade_Start);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }
    
    IEnumerator LoadCredits()
    {
        animator.Play(Fade_Start);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Waited");
        creditsAnimator.Play(Credits_Roll);
    }
    IEnumerator BackFromCreditsToMainMenu()
    {
        creditsAnimator.Play(Credits_Back);
        yield return new WaitForSeconds(1.5f);
        animator.Play(Fade_End);
    }
    #endregion
}
