using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{ 
[SerializeField] private MusicManager musicManager;


    public void Start()
    {
        musicManager.PlayMusic("MusicMenu");
    }

    public void PlayButton(string scenename) {

        SceneManager.LoadScene(scenename);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
