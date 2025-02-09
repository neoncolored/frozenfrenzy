using System;
using Managers;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScreenScripts
{
    public class MainMenu : MonoBehaviour

    {
        private void Start()
        {
            BackgroundMusic.Instance.PlaySong(0);
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("MainGame");
            BackgroundMusic.Instance.PlaySong(0);
            Player.Hp = 100;
        }

        public void Tutorial()
        {
            SceneManager.LoadScene("Tutorial");
            BackgroundMusic.Instance.PlaySong(0);
        }

        public void QuitGame()
        {
            Application.Quit();
            BackgroundMusic.Instance.StopSong(0);
        }
    }
}
