using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canHit;
    public KeyCode hitKey;
    public Rigidbody2D hitRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hitKey))
        {
            if(canHit)
            {
                hitRb.bodyType = RigidbodyType2D.Dynamic;
                hitRb.angularVelocity = 3f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Activator")
        {
            canHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canHit = false;
    }
}
