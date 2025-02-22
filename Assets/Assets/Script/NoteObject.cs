using System.Collections;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canHit;
    public KeyCode hitKey;
    private Rigidbody2D hitRb;
    private bool isHit = false;  // Prevent multiple hits
    private bool bothButtonsLogged;
    public GameObject goodEffect, hitEffect, perfectEffect, missEffect;

    void Start()
    {
        hitRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canHit && Input.GetKeyDown(hitKey) && !isHit)
        {
            isHit = true;  // Mark as hit so it doesn't get hit again
            HitNote();
        }

        CheckBothButtonsPressed();
    }

    private void CheckBothButtonsPressed()
    {
        if (InputManager.Instance != null && InputManager.Instance.AreBothButtonsPressed())
        {
            if (!bothButtonsLogged)
            {
                Debug.Log("Both buttons are pressed!");
                bothButtonsLogged = true;
            }
        }
        else
        {
            bothButtonsLogged = false;
        }
    }

    private void HitNote()
    {
        float posX = Mathf.Abs(transform.position.x);

        if (posX >= 0.50f)
        {
            GameManager.Instance.NormalHit();
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            Debug.Log("hit");
        }
        else if (posX > 0.25f)
        {
            GameManager.Instance.GoodHit();
            Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            Debug.Log("good hit");
        }
        else
        {
            GameManager.Instance.PerfectNote();
            Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            Debug.Log("perfect hit");
        }

        // Streak should increase only once per hit
        GameManager.Instance.NoteHit();

        ActivateGravity();
        StartCoroutine(WaitToDestroy());
    }

    private void ActivateGravity()
    {
        hitRb.gravityScale = 5f;
        hitRb.bodyType = RigidbodyType2D.Dynamic;
        hitRb.linearVelocity = new Vector2(hitRb.linearVelocity.x, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            canHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator") && !isHit)
        {
            canHit = false;
            GameManager.Instance.NoteMiss();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            StartCoroutine(WaitToDestroy());
        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
