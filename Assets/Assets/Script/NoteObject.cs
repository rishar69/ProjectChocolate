using System.Collections;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canHit;
    public KeyCode hitKey;
    private Rigidbody2D hitRb;
    private bool isHit = false;  
    private Collider2D hitCollider;
        public GameObject goodEffect, hitEffect, perfectEffect, missEffect;
    public Animator spriteAnimator;

    void Start()
    {
        hitCollider = GetComponent<Collider2D>();
        hitRb = GetComponent<Rigidbody2D>();
        spriteAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (canHit && Input.GetKeyDown(hitKey) && !isHit)
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
        Destroy(hitCollider);

        float posX = Mathf.Abs(transform.position.x);
        Vector3 posUp = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);


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
        spriteAnimator.SetTrigger("Dead");
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
            gameObject.tag = "HittedNote";
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
}
