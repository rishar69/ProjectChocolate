using UnityEngine;

public class ButtonHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color pressedSprite = Color.red;
    public Color originalSprite = Color.white;

    public KeyCode hitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hitButton))
        {
            spriteRenderer.color = pressedSprite; 
        }

        if (Input.GetKeyUp(hitButton))
        {
            spriteRenderer.color = originalSprite;
        }
    }
}
