using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneWithLoading(string sceneName)
    {
        if (!SceneExists(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' tidak ditemukan di Build Settings!");
            return;
        }

        // Hentikan dan hapus musik jika ada
        GameObject musicObj = GameObject.FindGameObjectWithTag("GameMusic");
        if (musicObj != null)
        {
            Destroy(musicObj);
        }

        // Simpan scene tujuan
        PlayerPrefs.SetString("NextScene", sceneName);
        SceneManager.LoadScene("LoadingScene");
    }

    bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
                return true;
        }
        return false;
    }
}
