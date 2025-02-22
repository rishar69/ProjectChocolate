using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject SpawnPointUp;
    [SerializeField]
    private GameObject SpawnPointDown;
    [SerializeField]
    private float EnemyInterval = 3.5f;
    [SerializeField]
    private GameObject NoteParent;
    int RNG = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy(EnemyInterval, EnemyPrefab, NoteParent.transform));
    }

    // Update is called once per frame
    void Update()
    {
        // RNG = RandomGeneratedNumber();
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy, Transform parent)
    {
        RNG = Random.Range(0, 2); // Generate a new random number for each spawn
        Debug.Log(RNG);
        yield return new WaitForSeconds(interval);
        if (RNG == 0)
        {
            GameObject newEnemy = Instantiate(enemy, SpawnPointUp.transform.position, Quaternion.identity, parent);
        }
        else
        {
            GameObject newEnemy = Instantiate(enemy, SpawnPointDown.transform.position, Quaternion.identity, parent);
        }
        StartCoroutine(SpawnEnemy(interval, enemy, parent)); // Continue spawning enemies
    }

    private int RandomGeneratedNumber()
    {
        return Random.Range(0, 2); // Returns 0 or 1
    }
}