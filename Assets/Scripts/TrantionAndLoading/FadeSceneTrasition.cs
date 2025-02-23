using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneTransition : MonoBehaviour
{
    public FadeTransition fadeTransition;

    public static FadeSceneTransition instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Supaya tidak hilang saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(string sceneName)
    {
        yield return fadeTransition.FadeOut();
        SceneManager.LoadScene(sceneName);
    }
}
