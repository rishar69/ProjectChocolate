using System.Collections;
using UnityEngine;

public class HordeObject : MonoBehaviour
{
    public bool canHit;
    public KeyCode hitKey = KeyCode.A;
    public KeyCode SecondHitKey = KeyCode.D;
    private Rigidbody2D hitRb;
    private bool isHit = false;
    private bool bothButtonsLogged;
    public GameObject nextNote;
    public GameObject goodEffect, hitEffect, perfectEffect, missEffect;

    void Start()
    {
        hitRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canHit && (Input.GetKeyDown(hitKey) || Input.GetKeyDown(SecondHitKey)) && !isHit)
        {
            isHit = true;
            HitNote();
        }

    }

    //private void CheckBothButtonsPressed()
    //{
    //    if (InputManager.Instance != null && InputManager.Instance.AreBothButtonsPressed())
    //    {
    //        if (!bothButtonsLogged)
    //        {
    //            Debug.Log("Both buttons are pressed!");
    //            bothButtonsLogged = true;
    //        }
    //    }
    //    else
    //    {
    //        bothButtonsLogged = false;
    //    }
    //}

    private void HitNote()
    {
        float posX = Mathf.Abs(transform.position.x);
        Vector3 posUp = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        FrontNoteIsHit();

        if (posX >= 0.50f)
        {
            GameManager.Instance.NormalHit();
            Instantiate(hitEffect, posUp, hitEffect.transform.rotation);
            Debug.Log("hit");
        }
        else if (posX > 0.25f)
        {
            GameManager.Instance.GoodHit();
            Instantiate(goodEffect, posUp, goodEffect.transform.rotation);
            Debug.Log("good hit");
        }
        else
        {
            GameManager.Instance.PerfectNote();
            Instantiate(perfectEffect, posUp, perfectEffect.transform.rotation);
            Debug.Log("perfect hit");
        }

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

    public void KillMySelf()
    {
        Destroy(gameObject);
    }
    public void FrontNoteIsHit()
    {
        transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
        if (nextNote != null)
        {
            nextNote.GetComponent<HordeObject>().FrontNoteIsHit();
        }
    }
}
