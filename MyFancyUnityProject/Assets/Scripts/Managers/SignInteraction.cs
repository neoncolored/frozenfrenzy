using ScreenScripts;
using UnityEngine;
using UnityEngine.UI;



public class SignInteraction : MonoBehaviour
{
    [Header("Pop-up UI, das 'Press E' anzeigt")]
    public GameObject ePrompt;  // Referenz auf dein kleines UI-Element

    [Header("Dialog-Text für dieses Schild")]
    [TextArea] public string signText = "Willkommen zum Tutorial!\nDrücke E, um den Text anzuzeigen.\nNochmals E, um zu schließen.";

    private bool isPlayerInRange = false;
    private bool isDialogueOpen = false;

    void Start()
    {
        if (ePrompt != null) 
            ePrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isDialogueOpen)
            {
                ePrompt.SetActive(false);
                DialogueManager.instance.ShowMenu(signText);
                isDialogueOpen = true;
            }
            else
            {
                DialogueManager.instance.CloseMenu();
                isDialogueOpen = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (ePrompt != null)
                ePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (ePrompt != null)
                ePrompt.SetActive(false);

            if (isDialogueOpen)
            {
                DialogueManager.instance.CloseMenu();
                isDialogueOpen = false;
            }
        }
    }
}