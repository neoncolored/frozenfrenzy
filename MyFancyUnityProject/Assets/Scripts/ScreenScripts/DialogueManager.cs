using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScreenScripts
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        public GameObject dialoguePanel;
        public TextMeshProUGUI dialogueText;
        public GameObject dialogueObject;
        private bool _isShowing = false;

        private void Awake()
        {
            instance = this;
            dialoguePanel.SetActive(false);
        }

        private void Update()
        {
            if(_isShowing && (Input.GetKeyDown(KeyCode.E))) CloseMenu();
        }

        public void ShowMenu(String text)
        {
            dialogueObject.GetComponent<DialogueBox>().ShowDialogue(text);
            dialoguePanel.SetActive(true);
            Time.timeScale = 0f;
            _isShowing = true;
        }
        
        public void CloseMenu()
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            _isShowing = false;
        }
        
    }
}