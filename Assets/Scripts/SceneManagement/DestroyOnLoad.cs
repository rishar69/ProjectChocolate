using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Jika objek ini sudah ada di scene lain, hapus
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
