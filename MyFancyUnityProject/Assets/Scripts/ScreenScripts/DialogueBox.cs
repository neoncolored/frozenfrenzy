using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScreenScripts
{
    public class DialogueBox : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public float typingSpeed = 0.05f;

        private void Awake()
        {

        }

        private void Start()
        {
            dialogueText.text = "";
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
                yield return null;
                
            }
        }
    }
}