using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;
    [SerializeField] private AudioClip[] songs;
    private AudioSource[] songsources = new AudioSource[3];
    
    private void Awake()
    {
        Instance = this;
        PlaySong(0);
    }

    public void PlaySong(int num)
    {
        if(songs[num] != null) songsources[num] = SoundFXManager.instance.PlaySoundFXClip(songs[num], transform, 0.3f, true, false);
    }

    public void StopSong(int num)
    {
        if(songsources[num] != null) songsources[num].Stop();
    }
    
    
    
}
