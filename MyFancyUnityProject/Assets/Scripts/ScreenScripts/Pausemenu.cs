using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class  PauseScreen : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button menuButton;

    public static bool gameIsPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        gameIsPaused = false;
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(GoToMainMenu);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (gameIsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("Scenes/Main Menu");
    }
}