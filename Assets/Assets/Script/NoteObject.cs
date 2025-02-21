using System.Collections;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canHit;
    public KeyCode hitKey;
    private Rigidbody2D hitRb;
    private bool isHit = true;
    private bool bothButtonsLogged = false; 

    void Start()
    {
        hitRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canHit && Input.GetKeyDown(hitKey))
        {
            HitNote();
        }

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
        isHit = true;
        //GameManager.Instance.NoteHit();
        if(Mathf.Abs(transform.position.x) >= 0.25)
        {
            GameManager.Instance.NormalHit();
        }
        else if(Mathf.Abs(transform.position.x) > 0.05f)
        {
            GameManager.Instance.GoodHit();
        }
        else
        {
            GameManager.Instance.PerfectNote();
        }

        hitRb.gravityScale = 5f;
        hitRb.bodyType = RigidbodyType2D.Dynamic;
        hitRb.linearVelocity = new Vector2(hitRb.linearVelocity.x, 20);
        

        StartCoroutine(WaitToDestroy());
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
            StartCoroutine(WaitToDestroy());
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this);
    }
}
