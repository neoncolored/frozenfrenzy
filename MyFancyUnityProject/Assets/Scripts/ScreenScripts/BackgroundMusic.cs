using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;
    [SerializeField] private AudioClip[] songs;
    
    private void Awake()
    {
        Instance = this;
        PlaySong(0);
    }

    public void PlaySong(int num)
    {
        if(songs[num] != null) SoundFXManager.instance.PlaySoundFXClip(songs[num], transform, 0.3f, true, false);
    }

    public void StopSong(int num)
    {
        
    }
    
    
    
}
