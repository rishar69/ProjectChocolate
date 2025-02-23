using System.Collections;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = 1 - (time / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    public IEnumerator FadeOut()
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = time / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
} 
