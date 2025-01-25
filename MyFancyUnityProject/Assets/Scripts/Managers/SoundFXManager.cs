using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawn, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        
        Destroy(audioSource.gameObject, clipLength);
    }
    
    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawn, float volume)
    {
        int random = UnityEngine.Random.Range(0, audioClip.Length);
        
        AudioSource audioSource = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
        audioSource.clip = audioClip[random];
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        
        Destroy(audioSource.gameObject, clipLength);
    }
}
