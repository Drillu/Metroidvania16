using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Animator creditsAnimator;
    public CanvasGroup creditsGroup;

    //Animations names
    const string Fade_End = "Fade_End";
    const string Fade_Start = "Fade_Start";
    const string Credits_Roll = "Credits_Roll";
    const string Credits_Back = "Credits_Back";

    //Sound
    public float musicParameter = 1f;
    public float sfxParameter = 1f;
    private int MusicLevel=6;
    private int SFXLevel=6;
    public List<Sprite> SoundLevels;
    [SerializeField] private Image MusicSlider;
    [SerializeField] private Image SFXSlider;


    #region Other than Options

    public void PlayButton()
    {
        Debug.Log("Play Button CLicked");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        StaticVariablesFromMenuToLevel.musicSound = musicParameter;
        StaticVariablesFromMenuToLevel.sfxSound = sfxParameter;
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

    #endregion

    #region Options

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("FULSCREEN: " + isFullscreen);
    }

    public void UpdateSounds()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LevelMusic",musicParameter);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LevelSFX", sfxParameter);
    }

    public void MusicPlusButton()
    {
        if(MusicLevel != SoundLevels.Count)
        {
            Debug.Log("Music Volume Up");
            MusicLevel++;
            musicParameter += 0.2f;
            MusicSlider.sprite = SoundLevels[MusicLevel - 1];
        }
    }

    public void MusicMinusButton()
    {
        if (MusicLevel != 1)
        {
            Debug.Log("Music Volume Down");
            MusicLevel--;
            musicParameter -= 0.2f;
            MusicSlider.sprite = SoundLevels[MusicLevel - 1];
        }
    }

    public void SFXPlusButton()
    {
        if (SFXLevel != SoundLevels.Count)
        {
            Debug.Log("SFX Volume Up");
            SFXLevel++;
            sfxParameter += 0.2f;
            SFXSlider.sprite = SoundLevels[SFXLevel - 1];
        }
    }

    public void SFXMinusButton()
    {
        if (SFXLevel != 1)
        {
            Debug.Log("SFX Volume Down");
            SFXLevel--;
            sfxParameter -= 0.2f;
            SFXSlider.sprite = SoundLevels[SFXLevel - 1];
        }
    }

    #endregion

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
