using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerGame : MonoBehaviour
{
    public void LoadNextLevel(string nextLevel)
    {
        if (ScreenFader.instance != null)
        {
            ScreenFader.instance.FadeToScene(nextLevel);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
