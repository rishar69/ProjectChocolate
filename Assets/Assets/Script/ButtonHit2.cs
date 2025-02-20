using UnityEngine;

public class ButtonHit2 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerController PlayerControll;
    public Color pressedSprite = Color.white;
    public Color originalSprite;
    public GameObject Player;
    public bool imUp = false;
    public bool imDown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControll = Player.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControll.isDouble || (PlayerControll.isUp && imUp) || (PlayerControll.isDown && imDown))
        {
            spriteRenderer.color = pressedSprite;
            Debug.Log("Tombol ditekan");
        }
        else
        {
            spriteRenderer.color = originalSprite;
        }
    }
}
