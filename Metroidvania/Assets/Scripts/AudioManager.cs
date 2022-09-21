using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    

    private void Start()
    {
        var gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Level1");
        gameMusic.setParameterByName("UnderGround", 1);
        gameMusic.start();
        var gameAmbiance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiance/AmbianceSurfaceLevel");
        gameAmbiance.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LevelMusic", StaticVariablesFromMenuToLevel.musicSound);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("LevelSFX", StaticVariablesFromMenuToLevel.sfxSound);


    }

    public static void PlaySound(string eventPath)
    {
        var temp = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        temp.start();
    }
    
    
}
