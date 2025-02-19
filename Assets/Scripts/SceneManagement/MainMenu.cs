using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{



    public void Start()
    {
        AudioManager.Instance.MusicManager.PlayMusic("MusicMenu");

    }

    public void PlayButton(string scenename)
    {

        SceneManager.LoadScene(scenename);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
