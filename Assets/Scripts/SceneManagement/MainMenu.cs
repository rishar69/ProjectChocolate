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
        AudioManager.Instance.MusicManager.PlayMusic("TestMusic", 0.5f);
        SceneManager.LoadScene("HitSystemTest");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
