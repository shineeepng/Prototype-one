using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 2f;

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            // Начинаем с черного экрана и плавно проявляем сцену
            fadeCanvasGroup.alpha = 2f;
            StartCoroutine(FadeIn());
        }
    }

    // Этот метод привязываем к кнопке Start
    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    // Плавное проявление (из черного в прозрачный)
    private IEnumerator FadeIn()
    {
        fadeCanvasGroup.blocksRaycasts = true; // Блокируем клики во время появления
        float timer = fadeDuration;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            fadeCanvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }

        fadeCanvasGroup.alpha = 0f;
        fadeCanvasGroup.blocksRaycasts = false; // Открываем UI для кликов
    }

    // Плавное затемнение (из прозрачного в черный) и загрузка
    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeCanvasGroup.blocksRaycasts = true; // Блокируем клики при уходе
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = timer / fadeDuration;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
        SceneManager.LoadScene(sceneName);
    }
}