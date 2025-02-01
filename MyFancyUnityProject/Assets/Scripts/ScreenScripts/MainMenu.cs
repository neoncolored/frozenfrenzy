using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour

{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
        BackgroundMusic.Instance.PlaySong(0);
        Player.hp = 100;
    }

    public void QuitGame()
    {
        Application.Quit();
        BackgroundMusic.Instance.StopSong(0);
    }
}
