using UnityEngine;

public class ButtonHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;
    private Vector3 buttonToTheLeftPos;

    public Color pressedSprite = Color.white;
    public Color originalSprite;
    public GameObject player;
    public KeyCode hitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hitButton))
        {
            playerAnimator.SetTrigger("pressed");
            spriteRenderer.color = pressedSprite;
            buttonToTheLeftPos = new Vector3(player.transform.position.x, transform.position.y, 0f);
            //player.transform.position = new Vector3(player.transform.position.x, transform.position.y, 0f);
            player.transform.position = Vector3.Lerp(player.transform.position,buttonToTheLeftPos,2f);
        }

        if (Input.GetKeyUp(hitButton))
        {
            spriteRenderer.color = originalSprite;
        }

    }
}
