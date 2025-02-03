using UnityEngine;

namespace Managers
{
    public class SoundFXManager : MonoBehaviour
    {
        public static SoundFXManager instance;

        [SerializeField] private AudioSource soundFXObject;
        [SerializeField] private AudioSource campfireSoundFXObject; 

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        public void PlaySoundFXClip(AudioClip audioClip, Transform spawn, float volume, bool loop, bool spatial)
        {
            AudioSource audioSource = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.spatialize = spatial;
            if (spatial)
            {
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.minDistance = 10;
                audioSource.maxDistance = 10.3f;
            }
        
            audioSource.Play();

        
            float clipLength = audioSource.clip.length;



            if (!loop)
            {
                Destroy(audioSource.gameObject, clipLength); 
            }
        }
    
        public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawn, float volume)
        {
            int random = UnityEngine.Random.Range(0, audioClip.Length-1);
        
            AudioSource audioSource = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
            audioSource.clip = audioClip[random];
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
        
            Destroy(audioSource.gameObject, clipLength);
        }
    
        public void PlayRandomSoundFXClipWithRandomPitch(AudioClip[] audioClip, Transform spawn, float volume)
        {
            int random = UnityEngine.Random.Range(0, audioClip.Length-1);
        
            AudioSource audioSource = Instantiate(soundFXObject, spawn.position, Quaternion.identity);
            audioSource.clip = audioClip[random];
            audioSource.pitch = UnityEngine.Random.Range(1f, 2f);
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;
        
            Destroy(audioSource.gameObject, clipLength);
        
        }

    }
}
