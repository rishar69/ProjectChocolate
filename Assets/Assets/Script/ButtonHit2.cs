using UnityEngine;

public class ButtonHit2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerController PlayerControll;
    public Color pressedSprite = Color.white;
    public Color originalSprite;
    public GameObject Player;
    public GameObject CollisionParrent;
    private bool imUp = false;
    private bool imDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControll = Player.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.color;
        if (PlayerControll.Up != null && PlayerControll.Down != null)
        {
            if (PlayerControll.Up == gameObject)
            {
                imUp = true;
            }
            else if (PlayerControll.Down == gameObject)
            {
                imDown = true;
            }
            // Debug.Log(gameObject);
            // Debug.Log(PlayerControll.Up);
            // Debug.Log(PlayerControll.Down);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControll.isDouble || (PlayerControll.isUp && imUp) || (PlayerControll.isDown && imDown))
        {
            spriteRenderer.color = pressedSprite;
            if (PlayerControll.isHitting)
            {
                if (CollisionParrent != null)
                {
                    CollisionParrent.GetComponent<NoteObject2>().Hit = true;
                    CollisionParrent = null;
                }
            }
            // Debug.Log("Tombol ditekan");
        }
        else
        {
            spriteRenderer.color = originalSprite;
        }
        // Debug.Log(CollisionParrent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            CollisionParrent = collision.gameObject;
            CollisionParrent.GetComponent<NoteObject2>().canHit = true;
            Debug.Log("Note masuk");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CollisionParrent != null)
        {
            if (collision.tag == "Note")
            {
                CollisionParrent.GetComponent<NoteObject2>().canHit = false;
                CollisionParrent = null;
                Debug.Log("Note keluar");
            }
        }
    }
}
