using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
        private Coroutine typing = null;
        private bool _isActive = false;

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
            if (typing != null)
            {
                StopCoroutine(typing);
                typing = null;
            }
            dialogueText.text = "";
            _isActive = true;
            typing = StartCoroutine(TypeText(message));
            
        }

        public void CloseDialogue()
        {
            if(typing != null) StopCoroutine(typing);
            typing = null;
            dialogueText.text = "";
            _isActive = false;
        }
        
        

        private IEnumerator TypeText(string message)
        {
            foreach (char letter in message.ToCharArray())
            {
                if (_isActive)
                {
                    dialogueText.text += letter;
                    SoundFXManager.instance.PlayRandomSoundFXClipWithRandomPitch(arr, transform, 0.07f);
                    yield return new WaitForSecondsRealtime(typingSpeed);
                }
                
            }
        }
    }
}