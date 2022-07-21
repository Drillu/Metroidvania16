using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance gameMusic;

    private void Start()
    {
        gameMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/InnerForest");
        gameMusic.start();
        var gameAmbiance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiance/AmbianceTemp");
        gameAmbiance.start();
        
    }

    public static void PlaySound(string eventPath)
    {
        var temp = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        temp.start();
    }
    
    
    
}
