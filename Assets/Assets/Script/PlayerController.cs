using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public int health;
    private void Start()
    {
        health = GameManager.Instance.health;
    }

    private void Update()
    {
        health = GameManager.Instance.health;
        if (health == 0)
        {
            GameManager.Instance.LevelFinish();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            GameManager.Instance.PlayerDamaged();
        }

    }
}

