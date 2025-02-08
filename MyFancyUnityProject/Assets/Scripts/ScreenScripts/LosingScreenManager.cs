using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScreenScripts
{
    public class LosingScreenManager : MonoBehaviour
    {
        public CanvasGroup losingScreenCanvasGroup; 
        public float fadeDuration = 1.5f;
        public Button restartButton;

        private bool _isFading = false;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        public void ShowLosingScreen()
        {
            if (!_isFading)
            {
                _isFading = true;
                StartCoroutine(FadeIn());
            }
        }

        private IEnumerator FadeIn()
        {
            var elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                losingScreenCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }

            losingScreenCanvasGroup.interactable = true;
            losingScreenCanvasGroup.blocksRaycasts = true;
        }

        private static void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Player.Hp = 100;
        }
    }
}

