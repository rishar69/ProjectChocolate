using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance; // Singleton agar mudah dipanggil dari scene lain
    private Image fadeImage;
    public float fadeDuration = 1f; // Durasi transisi

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan saat pindah scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        fadeImage = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // Mulai game dengan efek fade in
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOut(string sceneName)
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
