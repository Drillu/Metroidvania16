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
        
    }

    public static void PlaySound(string eventPath)
    {
        var temp = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        temp.start();
    }
    
    
}
