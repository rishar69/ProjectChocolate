using UnityEngine;

public class ButtonHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public KeyCode hitButton;
    public Color pressedSprite = Color.white;
    private Color originalSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.color;
    }

    public void SetPressedState(bool isPressed)
    {
        spriteRenderer.color = isPressed ? pressedSprite : originalSprite;
    }
}
