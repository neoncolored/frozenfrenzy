using UnityEngine;
using UnityEngine.UI;

public class SignInteraction : MonoBehaviour
{
    public GameObject textUI; 
    private bool isPlayerInRange = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (textUI != null)
            {
                textUI.SetActive(!textUI.activeSelf);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = true;
            Debug.Log("Player is near the sign!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInRange = false;
            Debug.Log("Player left the sign!");
            if (textUI != null)
            {
                textUI.SetActive(false);
            }
        }
    }
}