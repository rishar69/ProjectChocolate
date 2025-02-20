using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canHit;
    public KeyCode hitKey;
    private Rigidbody2D hitRb;

    void Start()
    {
        hitRb = GetComponent<Rigidbody2D>();

        // Subscribe to InputManager's event
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnButtonPressed += HandleInput;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnButtonPressed -= HandleInput;
        }
    }

    private void HandleInput(KeyCode pressedKey)
    {
        if (canHit && pressedKey == hitKey)
        {
            hitRb.gravityScale = 5f;
            hitRb.bodyType = RigidbodyType2D.Dynamic;
            hitRb.linearVelocity = new Vector2(hitRb.linearVelocity.x, 20);
        }
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
        if (collision.CompareTag("Activator"))
        {
            canHit = false;
        }
    }
}
