using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScreenScripts
{
    public class MainMenu : MonoBehaviour

    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync("MainGame");
            BackgroundMusic.Instance.PlaySong(0);
            Player.Hp = 100;
        }

        public void QuitGame()
        {
            Application.Quit();
            BackgroundMusic.Instance.StopSong(0);
        }
    }
}
