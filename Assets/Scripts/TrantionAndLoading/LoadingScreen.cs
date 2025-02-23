using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;  // Hubungkan di Inspector
    public Text progressText;   // Hubungkan di Inspector (Opsional)

    private string sceneToLoad;

    void Start()
    {
        // Pastikan UI tidak null sebelum digunakan
        if (progressBar == null)
        {
            Debug.LogError("Progress Bar tidak terhubung! Pastikan UI sudah dihubungkan di Inspector.");
            return;
        }
        if (progressText == null)
        {
            Debug.LogWarning("Progress Text tidak terhubung! Text persentase tidak akan ditampilkan.");
        }

        // Ambil scene tujuan dari PlayerPrefs
        sceneToLoad = PlayerPrefs.GetString("NextScene", "");

        // Cek apakah scene valid
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("Scene tujuan kosong! Pastikan scene telah disimpan sebelum loading.");
            return;
        }

        // Cek apakah scene ada di Build Settings
        if (!SceneExists(sceneToLoad))
        {
            Debug.LogError($"Scene '{sceneToLoad}' tidak ditemukan di Build Settings!");
            return;
        }

        // Mulai proses loading
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Jangan aktifkan scene langsung

        while (operation.progress < 0.9f)
        {
            float progress = operation.progress / 0.9f; // Normalisasi nilai (0-1)
            progressBar.value = progress;
            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progress * 100) + "%";

            yield return null;
        }

        // Tampilkan progress penuh
        progressBar.value = 1f;
        if (progressText != null)
            progressText.text = "100%";

        yield return new WaitForSeconds(1f);
        operation.allowSceneActivation = true; // Pindah ke scene tujuan
    }

    // Cek apakah scene ada di Build Settings
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
