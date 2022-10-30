using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static FMOD.Studio.EventInstance gameMusic;
    public static FMOD.Studio.EventInstance gameAmbiance;

    private void Start()
    {
        gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Level1");
        gameMusic.start();
        gameAmbiance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiance/AmbianceSurfaceLevel");
        gameAmbiance.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LevelMusic", StaticVariablesFromMenuToLevel.musicSound);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LevelSFX", StaticVariablesFromMenuToLevel.sfxSound);


    }

    public static void PlaySound(string eventPath)
    {
        var temp = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        temp.start();
    }
    
    public static void CutMusic()
    {
        //GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().
        gameMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        gameAmbiance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    
}
