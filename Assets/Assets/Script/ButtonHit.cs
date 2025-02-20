using UnityEngine;
using System.Collections;

public class ButtonHit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

    public Color pressedSprite = Color.white;
    public Color originalSprite;
    public GameObject player;
    public KeyCode hitButton;
    public float moveSpeed = 10f; // Speed of movement

    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(hitButton))
        {
            playerAnimator.SetTrigger("pressed");
            spriteRenderer.color = pressedSprite;

            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, 0f);

            // Stop any existing movement and start a new one
            StopAllCoroutines();
            StartCoroutine(MovePlayer(targetPosition));

            // Play sound safely
            if (AudioManager.SFXManager != null)
            {
                AudioManager.SFXManager.PlaySound2D("Miss");
            }
        }

        if (Input.GetKeyUp(hitButton))
        {
            spriteRenderer.color = originalSprite;
        }
    }

    IEnumerator MovePlayer(Vector3 target)
    {
        float distance = Vector3.Distance(player.transform.position, target);
        float duration = distance / moveSpeed; // Calculate duration based on speed
        float elapsedTime = 0f;
        Vector3 startPos = player.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            player.transform.position = Vector3.Lerp(startPos, target, t);
            yield return null;
        }

        // Ensure the player reaches the exact position
        player.transform.position = target;
    }
}
