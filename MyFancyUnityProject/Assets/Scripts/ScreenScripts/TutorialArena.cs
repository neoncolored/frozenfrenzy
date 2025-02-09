using System;
using UnityEngine;
using System.Collections;
using PlayerScripts;
using Waves;  // Falls du auf SampleWave.activeEnemies zugreifen willst
using ScreenScripts;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random; // Falls du auf DialogueManager zugreifen willst

public class TutorialArena : MonoBehaviour
{
    [Header("Gegner Prefabs (2D/3D)")]
    public GameObject[] enemiesToSpawn;    // Prefabs für deine 2-3 Gegner
    public Transform[] spawnPoints;        // Wo du die Gegner spawnen möchtest

    [Header("Dialog / UI")] 
    private string tutorialText = "I think I remember what my Penguin-Friends used to tell me..." +
                                 "\nUse left-click to shoot fish," +
                                 "\nRight-click to dash and damage enemies," +
                                 "\nAnd Left-Shift as a get-away!";
    
    private string campfire = "Ouch, that hurt! I should try healing next to a campfire...";
    private bool campfireTriggered = false;

    private string endOfTutorial = "That was awesome!" +
                                  "\nI think I'm ready for the real fight!" +
                                  "\nI'll just have to remember to heal next to the campfire." +
                                  "\nPress 'E' to continue!";
    public DialogueManager dialogueManager;
    private bool tutorialTriggered = false;
    private int aliveEnemies = 0;
    
    public GameObject moveText;
    private bool hasMoved = false;

    private void Start()
    {
        moveText.SetActive(true);
    }

    
    private void Update()
    {
        if (Player.Hp < 100 && !campfireTriggered)
        {
            campfireTriggered = true;
            dialogueManager.ShowMenu(campfire);
        }
        
        if (!hasMoved)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
            {
                hasMoved = true;

                if (moveText != null)
                {
                    new WaitForSecondsRealtime(2);
                    moveText.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !tutorialTriggered)
        {
            dialogueManager.ShowMenu(tutorialText);
            tutorialTriggered = true;
            StartCoroutine(SpawnTutorialEnemies());
        }
    }

    private IEnumerator SpawnTutorialEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var enemyPrefab = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            aliveEnemies++;
            SampleWave.activeEnemies++;
            yield return new WaitForSeconds(0.5f);
        }
        
        while (SampleWave.activeEnemies > 0)
        {
            yield return null; 
        }
        
        if (dialogueManager != null)
        {
            yield return new WaitForSeconds(2.0f);
            dialogueManager.ShowMenu(endOfTutorial);
            yield return new WaitForSeconds(2.0f);
            Player.Hp = 100;
            Player.Ammunition = 15;
            SceneManager.LoadScene("MainGame");
        }
    }
}
