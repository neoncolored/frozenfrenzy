using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

namespace ScreenScripts
{
    public class DialogueBox : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public AudioClip typingClip;
        private AudioClip[] arr = new AudioClip[1];
        public float typingSpeed = 0.05f;

        private void Awake()
        {

        }

        private void Start()
        {
            dialogueText.text = "";
            arr[0] = typingClip;
        }

        public void ShowDialogue(string message)
        {
            dialogueText.text = "";
            StartCoroutine(TypeText(message));
        }
        
        

        private IEnumerator TypeText(string message)
        {
            foreach (char letter in message.ToCharArray())
            {
                dialogueText.text += letter;
                SoundFXManager.instance.PlayRandomSoundFXClipWithRandomPitch(arr, transform, 0.1f);
                yield return new WaitForSecondsRealtime(typingSpeed);
                
            }
        }
    }
}